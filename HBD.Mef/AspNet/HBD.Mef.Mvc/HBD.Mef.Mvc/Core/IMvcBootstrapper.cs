#region using

using System;

#endregion

namespace HBD.Mef.Mvc.Core
{
    public interface IMvcBootstrapper : IDisposable
    {
        /// <summary>
        /// Initialize all the component and start the Bootstrapper
        /// This Method should be called when App Start.
        /// </summary>
        void Start();

        /// <summary>
        /// After app started this method will be called to register all areas resource like: css and javascripts.
        /// </summary>
        void PostStart();

        /// <summary>
        /// Release all component and shutdown the Bootstrapper.
        /// This method should be called when App Shutdown.
        /// </summary>
        void Shutdown();
    }
}