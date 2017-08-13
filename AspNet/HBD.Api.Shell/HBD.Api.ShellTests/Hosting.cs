using Castle.Core.Logging;
using TestStack.Seleno.Configuration;

namespace HBD.Api.ShellTests
{
    internal class Hosting
    {
        public static readonly SelenoHost Instance = new SelenoHost();

        static Hosting()
        {
            Instance.Run("../../../HBD.Api.Shell", 9000, c => c
                .UsingLoggerFactory(new ConsoleFactory())
            );
        }
    }
}