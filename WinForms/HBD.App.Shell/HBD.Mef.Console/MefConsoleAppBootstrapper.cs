namespace HBD.Mef.Console
{
    public abstract class MefConsoleAppBootstrapper : MefBootstrapper
    {
        private new void Run() => base.Run();

        public virtual void Run(params string[] args)
        {
            base.Run();
        }
    }
}