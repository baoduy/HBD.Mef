namespace HBD.Mef.Logging
{
    public interface ILogger
    {
        void Log(string message, LogCategory category);
    }
}
