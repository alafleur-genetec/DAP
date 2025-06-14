// Copyright 2024 Genetec Inc.
// Licensed under the Apache License, Version 2.0. See the LICENSE file.

namespace Genetec.Dap.CodeSamples.Server.ReportHandlers;

using System;

public class HealthEvent
{
    public int HealthEventId { get; set; }
    public int EventSourceTypeId { get; set; }
    public Guid SourceEntityGuid { get; set; }
    public string EventDescription { get; set; }
    public string MachineName { get; set; }
    public DateTime Timestamp { get; set; }
    public int SeverityId { get; set; }
    public int ErrorNumber { get; set; }
    public long Occurrence { get; set; }
    public Guid ObserverEntity { get; set; }
}