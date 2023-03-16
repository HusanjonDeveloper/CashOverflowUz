using System;
using CashOverflowUz.Brokers.Loggins;
using Microsoft.Extensions.Logging;

namespace CashOverflowUz.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public void LogError(Exception exception) =>
            logger.LogError(exception.Message, exception);

        public void LogCritical(Exception exception) =>
            logger.LogCritical(exception.Message, exception);

    }
}
