#region using

using HBD.Mef.Mvc;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;
using HBD.Mvc.Shell;
using HBD.Mvc.Shell.Controllers;
using WebActivatorEx;

#endregion

[assembly: PreApplicationStartMethod(typeof(Bootstrapper), "AppPreStart")]
[assembly: PostApplicationStartMethod(typeof(Bootstrapper), "AppPostStart")]
[assembly: ApplicationShutdownMethod(typeof(Bootstrapper), "AppShutdown")]

namespace HBD.Mvc.Shell
{
    public class Bootstrapper : MvcBootstrapper
    {
        #region The Bootstrapper Implementation

        protected override void RegisterMainNavigation(INavigationService maiNavigationService)
        {
            base.RegisterMainNavigation(maiNavigationService);

            maiNavigationService.AddNavigation(a => a.WithTitle("Home")
                .WithIcon(Glyphicon.glyphicon_home)
                .DisplayAt(0)
                .For<HomeController>(c => nameof(c.Index)));

            maiNavigationService.Menu("Setting")
                .AlignAtRight()
                .WithIcon(Glyphicon.glyphicon_cog)
                .DisplayIconOnly()
                .Items
                .AddNavigation(a => a.WithTitle("About")
                    .WithIcon(Glyphicon.glyphicon_info_sign)
                    .For<HomeController>(c => nameof(c.About)))
                .AddNavigation(a => a.WithTitle("Contact")
                    .WithIcon(Glyphicon.glyphicon_info_sign)
                    .For<HomeController>(c => nameof(c.Contact)))
                .AddSeperator()
                .AddViewInfo(i => i.WithIcon(Glyphicon.glyphicon_user).SetGetter(v =>
                {
                    if (v.User.Identity.IsAuthenticated)
                        return HBD.Framework.Core.UserPrincipalHelper.GetUserNameWithoutDomain( v.User.Identity.Name);
                    return "Guess";
                }));
        }

        #endregion

        #region Startup

        private static Bootstrapper Current { get; set; }

        public static void AppPreStart() => (Current = new Bootstrapper()).Start();
        public static void AppPostStart() => Current?.PostStart();
        public static void AppShutdown() => Current?.Shutdown();

        #endregion
    }
}