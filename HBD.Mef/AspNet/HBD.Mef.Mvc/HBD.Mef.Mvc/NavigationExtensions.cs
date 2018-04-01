#region

using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using HBD.Framework;
using HBD.Framework.Attributes;
using HBD.Framework.Core;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Icons;
using HBD.Mef.Mvc.Navigation;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#endregion

namespace HBD.Mef.Mvc
{
    public static class NavigationExtensions
    {
        #region Internal Methods

        public static bool IsValidRoles(this IMenuInfo @this, IPrincipal user)
        {
            var nav = @this as INavigateRole;
            if (nav == null || !nav.ReguireAuthorized) return true;
            if (!user.Identity.IsAuthenticated) return false;

            return nav.Roles.Count <= 0 || nav.Roles.Any(user.IsInRole);
        }

        #endregion

        #region Commond

        public static T WithTitle<T>(this T @this, [NotNull] string title) where T : ITitleObject
        {
            @this.Title = title;
            return @this;
        }

        public static T WithIcon<T>(this T @this, [NotNull] string fileName) where T : IConable
        {
            @this.Icon = fileName;
            return @this;
        }

        public static T WithIcon<T>(this T @this, Glyphicon icon) where T : IConable
        {
            @this.Icon = icon;
            return @this;
        }

        public static T WithIcon<T>(this T @this, FontAwesome icon) where T : IConable
        {
            @this.Icon = icon;
            return @this;
        }

        public static T AlignAtRight<T>(this T @this) where T : IDisplayability
        {
            @this.Alignment = MenuAlignment.Right;
            return @this;
        }

        public static T DisplayIconOnly<T>(this T @this) where T : IDisplayability
        {
            @this.DisplayType = MenuDisplayType.IconOnly;
            return @this;
        }

        public static T DisplayAt<T>(this T @this, ushort index) where T : IDisplayability
        {
            @this.DisplayIndex = index;
            return @this;
        }

        public static T WithAuthorize<T>(this T @this, params string[] roles) where T : INavigateRole
        {
            @this.ReguireAuthorized = true;
            @this.Roles.Clear();
            @this.Roles.AddRange(roles);
            return @this;
        }

        private static UrlHelper CreateUrlHelper()
        {
            var context = new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData());
            return new UrlHelper(context);
        }

        public static bool IsActive(this INavigationInfo @this, UrlHelper urlHelper)
        {
            var helper = urlHelper ?? CreateUrlHelper();

            if (@this is SeparetorInfo)
                return false;

            var nav = @this as NavigationInfo;
            if (nav == null) return false;

            var routeData = HttpContext.Current?.Request.RequestContext.RouteData;
            if (routeData == null) return false;

            var area = routeData.DataTokens["area"] as string ?? string.Empty;
            var controller = routeData.Values["controller"] as string ?? string.Empty;
            var action = routeData.Values["action"] as string ?? string.Empty;

            if (nav.IsRootLevel)
                return nav.AreaName.EqualsIgnoreCase(area)
                       && nav.Controller.EqualsIgnoreCase(controller);

            return nav.AreaName.EqualsIgnoreCase(area)
                   && nav.Controller.EqualsIgnoreCase(controller)
                   && nav.Action.EqualsIgnoreCase(action);

            //var virtualPath = helper.Action(nav.Action, nav.Controller, new {area = nav.AreaName}, null);
            //if (virtualPath == null) return false;

            //return virtualPath.Contains($"/{area}/{controller}");
        }

        public static bool IsActive(this MenuInfo @this, UrlHelper urlHelper)
        {
            return @this.Items.Any(i => i.IsActive(urlHelper));
        }

        private static void ValidateDuplication(this IEnumerable @this, string title)
        {
            Guard.ArgumentIsNotNull(title, nameof(title));
            if (@this.OfType<ITitleObject>().Any(a => a.Title.EqualsIgnoreCase(title)))
                throw new DuplicateNameException(title);
        }

        public static void For<TController>(this NavigationInfo @this, Func<TController, string> actionName)
            where TController : Controller
        {
            var controller = GetControllerName<TController>();
            var action = actionName.Invoke(null);
            @this.For(controller, action);

            if (@this.ReguireAuthorized) return;

            var att = GetRoles<TController>(action);
            @this.ReguireAuthorized = att.Item1;
            if (att.Item2 == null) return;
            @this.Roles.AddRange(att.Item2);
        }

        public static Tuple<bool, string[]> GetRoles<TController>(string action = null) where TController : Controller
        {
            return GetRoles(typeof(TController), action);
        }

        public static Tuple<bool, string[]> GetRoles([NotNull] Type controller, string action = null)
        {
            Guard.ArgumentIsNotNull(controller, nameof(controller));

            AuthorizeAttribute att = null;

            if (action.IsNotNullOrEmpty())
                // ReSharper disable once AssignNullToNotNullAttribute
                att = controller.GetMethod(action)?.GetCustomAttribute<AuthorizeAttribute>();
            if (att == null)
                att = controller.GetCustomAttribute<AuthorizeAttribute>();

            if (att == null) return new Tuple<bool, string[]>(false, null);

            return att.Roles.IsNullOrEmpty()
                ? new Tuple<bool, string[]>(true, null)
                : new Tuple<bool, string[]>(true, att.Roles.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries));
        }

        private static void For(this NavigationInfo @this, string controllerName, string actionName)
        {
            @this.Controller = NormalizeControllerName(controllerName);
            @this.Action = actionName;
            if (@this.Title.IsNullOrEmpty())
                @this.Title = @this.Action;
        }

        public static void SetGetter<TIn, TOut>(this LazyInfo<TIn, TOut> @this, Func<TIn, TOut> infoGetter)
        {
            @this.InfoGetter = infoGetter;
        }

        private static string GetControllerName<TController>() where TController : Controller
        {
            return NormalizeControllerName(typeof(TController).Name);
        }

        private static string NormalizeControllerName(string controllerName)
        {
            return controllerName.Replace("Controller", string.Empty);
        }

        #endregion

        #region NavigationCollection

        public static NavigationCollection AddNavigation([NotNull] this NavigationCollection @this,
            [NotNull] Action<NavigationInfo> info)
        {
            var nav = new NavigationInfo(@this.Parent.AreaName);
            info.Invoke(nav);

            @this.ValidateDuplication(nav.Title);
            @this.Add(nav);
            return @this;
        }


        public static NavigationCollection AddLink([NotNull] this NavigationCollection @this,
            [NotNull] Action<LinkInfo> info)
        {
            var nav = new LinkInfo(@this.Parent.AreaName);
            info.Invoke(nav);

            @this.ValidateDuplication(nav.Title);
            @this.Add(nav);
            return @this;
        }

        public static NavigationCollection AddSeperator([NotNull] this NavigationCollection @this)
        {
            @this.Add(new SeparetorInfo());
            return @this;
        }

        public static NavigationCollection AddViewInfo([NotNull] this NavigationCollection @this,
            [NotNull] Action<ViewInfo> info)
        {
            var nav = new ViewInfo(@this.Parent.AreaName);
            info.Invoke(nav);
            @this.Add(nav);
            return @this;
        }

        #endregion

        #region INavigationService

        public static INavigationService AddNavigation([NotNull] this INavigationService @this,
            [NotNull] Action<NavigationInfo> info)
        {
            var nav = new NavigationInfo(@this.AreaName) {IsRootLevel = true};
            info.Invoke(nav);

            @this.Items.ValidateDuplication(nav.Title);
            @this.Items.Add(nav);
            return @this;
        }


        public static INavigationService AddLink([NotNull] this INavigationService @this,
            [NotNull] Action<LinkInfo> info)
        {
            var nav = new LinkInfo(@this.AreaName);
            info.Invoke(nav);

            @this.Items.ValidateDuplication(nav.Title);
            @this.Items.Add(nav);
            return @this;
        }

        public static INavigationService AddViewInfo([NotNull] this INavigationService @this,
            [NotNull] Action<ViewInfo> info)
        {
            var nav = new ViewInfo(@this.AreaName);
            info.Invoke(nav);
            @this.Items.Add(nav);
            return @this;
        }

        #endregion

        #region IFouterNavigation

        public static IFooterNavigationService AddNavigation([NotNull] this IFooterNavigationService @this,
            string areaName,
            [NotNull] Action<NavigationInfo> info)
        {
            var nav = new NavigationInfo(areaName);
            info.Invoke(nav);

            @this.Items.ValidateDuplication(nav.Title);
            @this.Items.Add(nav);
            return @this;
        }


        public static IFooterNavigationService AddLink([NotNull] this IFooterNavigationService @this,
            string areaName, [NotNull] Action<LinkInfo> info)
        {
            var nav = new LinkInfo(areaName);
            info.Invoke(nav);

            @this.Items.ValidateDuplication(nav.Title);
            @this.Items.Add(nav);
            return @this;
        }

        public static IFooterNavigationService AddLink([NotNull] this IFooterNavigationService @this,
            [NotNull] Action<LinkInfo> info)
        {
            var nav = new LinkInfo();
            info.Invoke(nav);

            @this.Items.ValidateDuplication(nav.Title);
            @this.Items.Add(nav);
            return @this;
        }

        public static IFooterNavigationService AddViewInfo([NotNull] this IFooterNavigationService @this,
            string areaName, [NotNull] Action<ViewInfo> info)
        {
            var nav = new ViewInfo(areaName);
            info.Invoke(nav);
            @this.Items.Add(nav);
            return @this;
        }

        public static IFooterNavigationService AddViewInfo([NotNull] this IFooterNavigationService @this,
            [NotNull] Action<ViewInfo> info)
        {
            var nav = new ViewInfo(null);
            info.Invoke(nav);
            @this.Items.Add(nav);
            return @this;
        }

        #endregion
    }
}