using Renetdux.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Renetdux.Infrastructure.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public LoggerService(Configuration configuration)
        {
            // Use any log service here
        }

        public void LogEvent(string name, Dictionary<string, string> props = null)
        {
            
        }

        public void LogTrace(string message, SeverityLevel level, IDictionary<string, string> properties = null)
        {
            
        }

        public void LogException(Exception ex, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null)
        {
            
        }
    }
}
