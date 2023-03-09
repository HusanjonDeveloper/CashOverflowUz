using System;

namespace CashOverflowUz.Brokers.Loggins
{
    public interface ILoggingBroker
    {
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
