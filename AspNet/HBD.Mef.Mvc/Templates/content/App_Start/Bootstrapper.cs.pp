#region using

using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof($rootnamespace$.Bootstrapper), "AppPreStart")]
[assembly: PostApplicationStartMethod(typeof($rootnamespace$.Bootstrapper), "AppPostStart")]
[assembly: ApplicationShutdownMethod(typeof($rootnamespace$.Bootstrapper), "AppShutdown")]
namespace $rootnamespace$
{
    public class Bootstrapper : MvcBootstrapper
    {
        #region The Bootstrapper Implementation

        protected override void RegisterMainNavigation(INavigationService maiNavigationService)
        {
            base.RegisterMainNavigation(maiNavigationService);

            /* The sample to register the menu for Shell.
			maiNavigationService.AddNavigation(a => a.WithTitle("Home")
                .WithIcon(Glyphicon.glyphicon_home)
                .DisplayAt(0)
                .For<HomeController>(c => nameof(c.Index)));
				*/
        }

        #endregion

        #region Startup

        private static Bootstrapper Current { get; set; }

		/// <summary>
        /// Get exported object from Mef Container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetExportedOrDefault<T>()
            => Current.Container.GetExportedValueOrDefault<T>();

        public static void AppPreStart() => (Current = new Bootstrapper()).Start();
        public static void AppPostStart() => Current.PostStart();
        public static void AppShutdown() => Current.Shutdown();

        #endregion
    }
}