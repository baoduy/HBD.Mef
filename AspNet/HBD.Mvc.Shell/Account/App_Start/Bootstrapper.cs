#region using

using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof(Account.Bootstrapper), "AppPreStart")]
[assembly: PostApplicationStartMethod(typeof(Account.Bootstrapper), "AppPostStart")]
[assembly: ApplicationShutdownMethod(typeof(Account.Bootstrapper), "AppShutdown")]
namespace Account
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