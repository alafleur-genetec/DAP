// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using Prism.Commands;
    using Sdk;
    using Sdk.Credentials;
    using Sdk.Entities;
    using Sdk.Queries;
    using Sdk.Workspace.Components.CredentialReader;

    public class SampleCardholderCredentialReader : CardholderCredentialReader, INotifyPropertyChanged
    {
        private int m_facilityCode;

        private int m_quantity;

        private int m_startNumber;

        public SampleCardholderCredentialReader()
        {
            GenerateCommand = new DelegateCommand(async () =>
            {
                List<WiegandStandardCredentialFormat> formats = Enumerable.Range(StartNumber, Quantity).Select(i => new WiegandStandardCredentialFormat(FacilityCode, i)).ToList();

                var query = (CredentialConfigurationQuery)Workspace.Sdk.ReportManager.CreateReportQuery(ReportType.CredentialConfiguration);

                foreach (WiegandStandardCredentialFormat format in formats)
                {
                    query.UniqueIds.Add(format);
                }

                QueryCompletedEventArgs result = await Task.Factory.FromAsync(query.BeginQuery, query.EndQuery, null);
                var existing = result.Data.AsEnumerable().Select(row => row.Field<Guid>(nameof(Guid)));

                foreach (Guid entityId in existing)
                {
                    OnExistingCredentialRetrieved(new ExistingCredentialRetrievedEventArgs(entityId));
                }

                var missing = formats
                    .Except(existing.Select(Workspace.Sdk.GetEntity).OfType<Credential>().Select(credential => credential.Format), new CredentialFormatComparer())
                    .OfType<WiegandStandardCredentialFormat>();

                await Workspace.Sdk.TransactionManager.ExecuteTransactionAsync(() =>
                {
                    foreach (WiegandStandardCredentialFormat format in missing)
                    {
                    
                        Dispatcher.Invoke(() => OnCredentialRetrieved(new CredentialRetrievedEventArgs(format)));
                    }
                });
            });
        }

        public override string Name => nameof(SampleCardholderCredentialReader);

        public override Guid UniqueId { get; } = new Guid("9B28081F-206B-41D4-AC26-B358DD9573EF");

        public override bool IsValid => true;

        public override OperationMode OperationMode { get; set; }

        public int StartNumber
        {
            get => m_startNumber;
            set => SetProperty(ref m_startNumber, value);
        }

        public int FacilityCode
        {
            get => m_facilityCode;
            set => SetProperty(ref m_facilityCode, value);
        }

        public int Quantity
        {
            get => m_quantity;
            set => SetProperty(ref m_quantity, value);
        }

        public DelegateCommand GenerateCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void Dispose()
        {
        }

        public override void Activate()
        {
        }

        public override UIElement CreateView()
        {
            return new CredentialReaderView(this);
        }

        public override void Deactivate()
        {
        }

        public override bool SupportsOperationMode(OperationMode mode)
        {
            return mode != OperationMode.Retrieval;
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}