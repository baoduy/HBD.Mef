#region using

using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof(HBD.Api.Shell.Bootstrapper), "AppPreStart")]
[assembly: PostApplicationStartMethod(typeof(HBD.Api.Shell.Bootstrapper), "AppPostStart")]
[assembly: ApplicationShutdownMethod(typeof(HBD.Api.Shell.Bootstrapper), "AppShutdown")]
namespace HBD.Api.Shell
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

        public static void AppPreStart() => (Current = new Bootstrapper()).Start();
        public static void AppPostStart() => Current.PostStart();
        public static void AppShutdown() => Current.Shutdown();

        #endregion
    }
}