// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples
{
    using Genetec.Sdk.Workspace.Modules;

    public class SampleModule : Module
    {
        public override void Load()
        {
            var component = new SampleBadgePrinter();
            component.Initialize(Workspace);
            Workspace.Components.Register(component);
        }

        public override void Unload()
        {
        }
    }
}