﻿// Copyright (C) 2023 by Genetec, Inc. All rights reserved.
// May be used only in accordance with a valid Source Code License Agreement.

namespace Genetec.Dap.CodeSamples
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Sdk;
    using Sdk.Entities;
    using Sdk.Media.Export;
    using File = System.IO.File;

    public class VideoExport
    {
        private readonly IEngine m_engine;

        public VideoExport(IEngine engine) => m_engine = engine;

        public Task Export(IEnumerable<Camera> cameras, Sdk.Media.DateTimeRange range, string fileName, ExportOption option, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            IEnumerable<CameraExportConfig> configs = cameras.Select(camera => new CameraExportConfig(camera.Guid, Enumerable.Repeat(range, 1)));

            return option.Format != VideoExportFormat.G64x
                ? Task.WhenAll(configs.Select(config => Export(Enumerable.Repeat(config, 1), fileName, option, progress, cancellationToken)))
                : Export(configs, fileName, option, progress, cancellationToken);
        }

        public async Task Export(IEnumerable<CameraExportConfig> configs, string fileName, ExportOption option, IProgress<double> progress = null, CancellationToken cancellationToken = default)
        {
            var exporter = new MediaExporter();
            exporter.StatisticsReceived += OnStatisticsReceived;
            try
            {
                exporter.Initialize(m_engine, Path.GetDirectoryName(fileName));
                exporter.SetExportFileFormat(option.Format == VideoExportFormat.G64 ? MediaExportFileFormat.G64 : MediaExportFileFormat.G64X);

                ExportEndedResult result = await exporter.ExportAsync(configs, option.PlaybackMode, Path.GetFileNameWithoutExtension(fileName), option.IncludeWatermark);

                if (result.ExceptionDetails != null)
                    throw result.ExceptionDetails;

                await Task.WhenAll(result.ExportFileList.Select(filePath => Convert(filePath, option.Format, new Progress<int>(value => progress?.Report(value)))));
            }
            finally
            {
                exporter.StatisticsReceived -= OnStatisticsReceived;
                exporter.Dispose();
            }

            void OnStatisticsReceived(object sender, ExportStatisticsEventArgs args) => progress?.Report(args.ExportPercentComplete);

            async Task Convert(string filePath, VideoExportFormat format, IProgress<int> convertProgress)
            {
                switch (format)
                {
                    case VideoExportFormat.Asf:
                    using (var converter = new G64ToAsfConverter())
                    {
                        converter.Initialize(
                            m_engine,
                            filePath,
                            false,
                            false,
                            option.ExportAudio,
                            GetOutputFilePath(), G64ToAsfConverter.GetAsfProfiles().First().Profile);

                        await ConvertAsync(converter);
                    }

                    break;

                    case VideoExportFormat.MP4:
                    using (var converter = new G64ToMp4Converter())
                    {
                        converter.Initialize(
                            m_engine,
                            filePath,
                            option.ExportAudio,
                            GetOutputFilePath());

                        await ConvertAsync(converter);
                    }

                    break;
                }

                string GetOutputFilePath()
                {
                    string path = Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}.{format.ToString().ToLower()}");
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    return path;
                }

                Task ConvertAsync(G64ConverterBase converter) => converter.ConvertAsync(convertProgress, cancellationToken).ContinueWith(task => File.Delete(filePath), cancellationToken);
            }
        }
    }
}