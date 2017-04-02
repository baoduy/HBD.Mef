#region

using HBD.Framework;
using HBD.Framework.Core;
using HBD.Mef.Shell.Core;
using HBD.Mef.Shell.Navigation;
using System;
using System.Linq;

#endregion

namespace HBD.Mef.Shell
{
    public static class NavigationExtensions
    {
        #region Properties Methods

        public static T WithIcon<T>(this T @this, object icon) where T : Iconable
        {
            @this.Icon = icon;
            return @this;
        }

        public static T WithToolTip<T>(this T @this, string toolTip) where T : IToolTipable
        {
            @this.ToolTip = toolTip;
            return @this;
        }

        public static T WithAlignment<T>(this T @this, MenuAlignment alignment) where T : IMenuInfo
        {
            @this.Alignment = alignment;
            return @this;
        }

        public static T WithName<T>(this T @this, string name) where T : INamedObject
        {
            @this.Name = name;
            return @this;
        }

        public static T WithTitle<T>(this T @this, string title) where T : ITitleObject
        {
            @this.Title = title;
            return @this;
        }

        public static T DisplayIconOnly<T>(this T @this) where T : MenuInfoBase
        {
            @this.DisplayMode = DisplayMode.IconOnly;
            return @this;
        }

        public static T DisplayAt<T>(this T @this, int index) where T : IMenuInfo
        {
            if (@this.Parent == null)
                throw new InvalidOperationException("Parent Collection should not be Null.");

            @this.DisplayIndex = index;

            return @this;
        }

        /// <summary>
        /// Set Access Roles to the MenuItem. The existing Roles will be removed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="permissionValidation"></param>
        /// <returns></returns>
        public static T AndValidFor<T>(this T @this, IPermissionValidationInfo permissionValidation) where T : IMenuInfo
        {
            @this.PermissionValidation = permissionValidation;
            return @this;
        }

        #endregion

        #region Privates

        internal static IMenuInfo GetItemByTitleOrName(this IMenuInfoCollection @this, string titleOrName)
            => @this.FirstOrDefault(m => m.CastAs<INamedObject>()?.Name.EqualsIgnoreCase(titleOrName) == true
                                         || m.CastAs<ITitleObject>()?.Title.EqualsIgnoreCase(titleOrName) == true);

        #endregion Privates

        #region Removes

        public static bool Remove(this IMenuInfoCollection @this, string title)
        {
            var menu = @this.OfType<ITitleObject>().FirstOrDefault(m => m.Title.EqualsIgnoreCase(title));
            if (menu == null)
                return false;

            @this.Remove((IMenuInfo)menu);
            return true;
        }

        #endregion Removes

        #region Add/Insert

        internal static MenuInfo AddMenu(this IMenuInfoCollection @this, string title, string name = null)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new MenuInfo(@this) { Title = title, Name = name ?? title };
            @this.Add(m);
            return m;
        }

        public static NavigationInfo AddNavigation(this IMenuInfoCollection @this, string title, string name = null)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new NavigationInfo(@this) { Title = title, Name = name ?? title };
            @this.Add(m);
            return m;
        }

        public static IMenuInfoCollection AddSeparator(this IMenuInfoCollection @this)
        {
            var s = new SeparatorInfo(@this);
            @this.Add(s);
            return @this;
        }

        public static NavigationParameterCollector<INavigationInfo> For(this INavigationInfo @this,
            INavigationParameter parameter)
        {
            Guard.ArgumentIsNotNull(@this, typeof(INavigationInfo).Name);
            Guard.ArgumentIsNotNull(parameter, nameof(parameter));

            @this.NavigationParameters.Add(parameter);
            return new NavigationParameterCollector<INavigationInfo>(@this);
        }

        public static NavigationParameterCollector<INavigationInfo> ForAction(this INavigationInfo @this, Action action)
        {
            Guard.ArgumentIsNotNull(@this, typeof(INavigationInfo).Name);
            Guard.ArgumentIsNotNull(action, nameof(action));

            @this.NavigationParameters.Add(new ActionNavigationParameter(action));
            return new NavigationParameterCollector<INavigationInfo>(@this);
        }

        public static NavigationParameterCollector<INavigationInfo> ForView(this INavigationInfo @this, Type viewType,
            string viewName = null)
        {
            Guard.ArgumentIsNotNull(@this, typeof(INavigationInfo).Name);
            Guard.ArgumentIsNotNull(viewType, nameof(viewType));

            @this.NavigationParameters.Add(new ViewInfo(viewName, viewType));
            return new NavigationParameterCollector<INavigationInfo>(@this);
        }

        public static MenuInfo AndMenu(this IParentable<IMenuInfoCollection> @this, string title)
            => @this.Parent.Menu(title);

        public static NavigationInfo AndNavigation(this IParentable<IMenuInfoCollection> @this, string title)
            => @this.Parent.Navigation(title);

        public static IMenuInfoCollection AndSeparator(this IParentable<IMenuInfoCollection> @this)
        {
            @this.Parent.AddSeparator();
            return @this.Parent;
        }

        public static MenuInfo AndMenu(this NavigationParameterCollector<INavigationInfo> @this, string title)
            => @this.NavigationInfo.Parent.Menu(title);

        public static NavigationInfo AndNavigation(this NavigationParameterCollector<INavigationInfo> @this,
                string title)
            => @this.NavigationInfo.Parent.Navigation(title);

        public static IMenuInfoCollection AndSeparator(this NavigationParameterCollector<INavigationInfo> @this)
        {
            @this.NavigationInfo.Parent.AddSeparator();
            return @this.NavigationInfo.Parent;
        }

        #endregion Add/Insert

        #region Gets

        public static bool Contains(this IMenuInfoCollection @this, string title)
            => @this.GetItemByTitleOrName(title) != null;

        /// <summary>
        ///     Get MenuInfo by title. The new MenuInfo will be added if not existed.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="titleOrName"></param>
        /// <returns></returns>
        public static MenuInfo Menu(this IMenuInfoCollection @this, string titleOrName)
        {
            var menu = @this.GetItemByTitleOrName(titleOrName);
            if (menu == null)
                return @this.AddMenu(titleOrName);
            return (MenuInfo)menu;
        }

        /// <summary>
        ///     Get NavigationInfo by title. The new NavigationInfo will be added if not existed.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="titleOrName"></param>
        /// <returns></returns>
        public static NavigationInfo Navigation(this IMenuInfoCollection @this, string titleOrName)
        {
            var menu = @this.GetItemByTitleOrName(titleOrName);
            if (menu == null)
                return @this.AddNavigation(titleOrName);
            return (NavigationInfo)menu;
        }

        #endregion Gets
    }
}