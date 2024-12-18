﻿// Copyright (C) 2023 by Genetec, Inc. All rights reserved.
// May be used only in accordance with a valid Source Code License Agreement.

namespace Genetec.Dap.CodeSamples
{
    using Sdk.Workspace.Modules;

    public class SampleModule : Module
    {
        static SampleModule() => AssemblyResolver.Initialize();

        public override void Load()
        {
            var component = new SampleImageExtractor();
            component.Initialize(Workspace);
            Workspace.Components.Register(component);
        }

        public override void Unload()
        {
        }
    }
}