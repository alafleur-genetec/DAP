// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples
{
    using System;
    using Sdk.Workspace.Components;
    using Sdk.Workspace.Pages.Contents;
    using Sdk.Workspace.Components.TileView;

    public class SampleTileViewBuilder : TileViewBuilder
    {
        public override string Name => nameof(SampleTileViewBuilder);

        public override Guid UniqueId { get; } = new Guid("BA77CE7D-51D5-4274-B75F-0BCEEC392F03");

        public override TileView CreateView()
        {
            return new SampleTileView();
        }

        public override bool IsSupported(TilePluginContext context)
        {
            return context.Content is EntityContent || context.ContentGroup is EntityContentGroup;
        }
    }
}