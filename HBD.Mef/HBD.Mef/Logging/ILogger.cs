using System;

namespace HBD.Mef.Logging
{
    public interface ILogger : IDisposable
    {
        void Log(string message, LogCategory category);
    }
}