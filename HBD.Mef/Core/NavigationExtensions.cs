using System;
using System.Linq;
using HBD.Framework;
using HBD.Framework.Core;

namespace HBD.Mef.Core.Navigation
{
    public static class NavigationExtensions
    {
        public static T WithIcon<T>(this T @this, object icon) where T : Iconable
        {
            @this.Icon = icon;
            return @this;
        }

        //public static T WithTitle<T>(this T @this, string title) where T : ITitleObject
        //{
        //    @this.Title = title;
        //    return @this;
        //}

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

        #region Privates

        private static IMenuInfo GetItemByTitle(this IMenuInfoCollection @this, string title)
            => (IMenuInfo) @this.OfType<ITitleObject>().FirstOrDefault(m => m.Title.EqualsIgnoreCase(title));

        #endregion Privates

        #region Removes

        public static bool Remove(this IMenuInfoCollection @this, string title)
        {
            var menu = @this.OfType<ITitleObject>().FirstOrDefault(m => m.Title.EqualsIgnoreCase(title));
            if (menu == null)
                return false;

            @this.Remove((IMenuInfo) menu);
            return true;
        }

        #endregion Removes

        #region Add/Insert

        public static MenuInfo AddMenu(this IMenuInfoCollection @this, string title)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new MenuInfo(@this) {Title = title};
            @this.Add(m);
            return m;
        }

        public static NavigationInfo AddNavigation(this IMenuInfoCollection @this, string title)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new NavigationInfo(@this) {Title = title};
            @this.Add(m);
            return m;
        }

        public static IMenuInfoCollection AddSeparator(this IMenuInfoCollection @this)
        {
            var s = new SeparatorInfo(@this);
            @this.Add(s);
            return @this;
        }

        public static MenuInfo InsertMenu(this IMenuInfoCollection @this, int index, string title)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new MenuInfo(@this) {Title = title};
            @this.Insert(index, m);
            return m;
        }

        public static NavigationInfo InsertNavigation(this IMenuInfoCollection @this, int index, string title)
        {
            Guard.ArgumentIsNotNull(@this, typeof(IMenuInfoCollection).Name);
            Guard.ArgumentIsNotNull(title, nameof(title));

            var m = new NavigationInfo(@this) {Title = title};
            @this.Insert(index, m);
            return m;
        }

        public static IMenuInfoCollection InsertSeparator(this IMenuInfoCollection @this, int index)
        {
            var s = new SeparatorInfo(@this);
            @this.Insert(index, s);
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
            => @this.GetItemByTitle(title) != null;

        /// <summary>
        ///     Get MenuInfo by title. The new MenuInfo will be added if not existed.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static MenuInfo Menu(this IMenuInfoCollection @this, string title)
        {
            var menu = @this.GetItemByTitle(title);
            if (menu == null)
                return @this.AddMenu(title);
            return (MenuInfo) menu;
        }

        /// <summary>
        ///     Get NavigationInfo by title. The new NavigationInfo will be added if not existed.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static NavigationInfo Navigation(this IMenuInfoCollection @this, string title)
        {
            var menu = @this.GetItemByTitle(title);
            if (menu == null)
                return @this.AddNavigation(title);
            return (NavigationInfo) menu;
        }

        #endregion Gets
    }
}