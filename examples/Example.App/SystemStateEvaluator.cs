﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using X.Spectator.Base;

namespace Example.App;

public class SystemStateEvaluator : IStateEvaluator<HealthStatus>
{
    public HealthStatus Evaluate(HealthStatus currentState, DateTime stateChangedLastTime, IReadOnlyCollection<JournalRecord> journal)
    {
        var last = journal.LastOrDefault();

        if (last == null)
        {
            return HealthStatus.Healthy;
        }
        
        return last.Values.Any(o => o.Status == HealthStatus.Unhealthy)
            ? HealthStatus.Degraded
            : HealthStatus.Healthy;
    }
}