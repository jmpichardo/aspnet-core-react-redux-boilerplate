using System;
using System.Collections.Generic;

namespace Renetdux.Infrastructure.Services.Logger
{
    public interface ILoggerService
    {
        void LogTrace(string message, SeverityLevel level, IDictionary<string, string> props = null);

        void LogException(Exception ex, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null);

        void LogEvent(string name, Dictionary<string, string> props = null);
    }
}
