#region

using System.Collections.ObjectModel;
using System.Linq;
using HBD.Framework;
using HBD.Mef.Mvc.Core;
using HBD.Mef.Mvc.Navigation.NavigateInfo;

#region using

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using HBD.Framework.Attributes;
using HBD.Framework.Core;
using System.Security.Principal;

#endregion

#endregion

namespace HBD.Mef.Mvc.Navigation
{
    [Export(typeof(INavigationServiceFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NavigationServiceFactory : INavigationServiceFactory
    {
        private readonly IServiceLocator _container;
        private readonly object _locker = new object();
        private bool _isMerged;
        private List<IMenuInfo> _leftItems;
        private List<IMenuInfo> _rightItems;

        [ImportingConstructor]
        public NavigationServiceFactory([NotNull] IServiceLocator container)
        {
            Guard.ArgumentIsNotNull(container, nameof(container));
            _container = container;
            RegisterdArea = new ConcurrentDictionary<string, INavigationService>();
        }

        public ConcurrentDictionary<string, INavigationService> RegisterdArea { get; }

        protected ICollection<IMenuInfo> LeftMenuItems
        {
            get
            {
                MergeAndShortMenuInfos();
                return _leftItems;
            }
        }

        protected ICollection<IMenuInfo> RightMenuItems
        {
            get
            {
                MergeAndShortMenuInfos();
                return _rightItems;
            }
        }

        public virtual INavigationService CreateNavigationService(string areaName)
        {
            Guard.ArgumentIsNotNull(areaName, nameof(areaName));

            lock (RegisterdArea)
            {
                return RegisterdArea.GetOrAdd(areaName, CreateService);
            }
        }

        public virtual bool IsRegistered(string areaName)
        {
            return RegisterdArea.ContainsKey(areaName);
        }

        public ICollection<IMenuInfo> GetLeftMenuFor(IPrincipal user)
        {
            return GetMenuItemFor(LeftMenuItems, user);
        }

        public ICollection<IMenuInfo> GetRightMenuFor(IPrincipal user)
        {
            return GetMenuItemFor(RightMenuItems, user);
        }

        protected ICollection<IMenuInfo> GetMenuItemFor(ICollection<IMenuInfo> originalList, IPrincipal user)
        {
            var list = new Collection<IMenuInfo>();

            foreach (var menuInfo in originalList)
            {
                var newItem = menuInfo.Clone();

                if (newItem is INavigationInfo)
                {
                    list.Add(newItem);
                    continue;
                }

                var info = newItem as MenuInfo;
                if (info == null) continue;
                var i = 0;

                while (i < info.Items.Count)
                {
                    var item = info.Items[i];

                    if (!item.IsValidRoles(user))
                    {
                        info.Items.Remove(item);
                        continue;
                    }

                    i++;
                }

                ConsolidateSeparetorInfo(info);

                if (info.Items.Count <= 0) continue;
                list.Add(info);
            }

            return list;
        }

        protected virtual INavigationService CreateService(string areaName)
        {
            var instance = _container.GetInstance<INavigationService>();
            instance.AreaName = areaName;
            return instance;
        }

        internal INavigationService GetMainNavigationService()
        {
            lock (RegisterdArea)
            {
                return RegisterdArea.GetOrAdd(string.Empty, CreateService);
            }
        }

        protected virtual void MergeAndShortMenuInfos()
        {
            lock (_locker)
            {
                if (_isMerged) return;
                _isMerged = true;

                if (_leftItems == null)
                    _leftItems = new List<IMenuInfo>();
                else _leftItems.Clear();

                if (_rightItems == null)
                    _rightItems = new List<IMenuInfo>();
                else _rightItems.Clear();

                foreach (var m in RegisterdArea.Values.SelectMany(s => s.Items))
                {
                    if (m == null) continue;
                    var d = (IDisplayability) m;
                    MergeMenuItem(d.Alignment == MenuAlignment.Left ? _leftItems : _rightItems, m);
                }

                var comparer = new MenuComparer();
                _leftItems.Sort(comparer);
                _rightItems.Sort(comparer);
            }
        }

        private static void MergeMenuItem(ICollection<IMenuInfo> listItem, IMenuInfo item)
        {
            var info = item as MenuInfo;
            if (info == null)
            {
                listItem.Add(item);
                return;
            }

            var found = listItem.OfType<MenuInfo>()
                .FirstOrDefault(i => i.Title.EqualsIgnoreCase(info.Title));

            if (found == null)
            {
                ConsolidateSeparetorInfo(info);
                listItem.Add(item);
                return;
            }

            //Re-order the item by new Index.
            if (info.DisplayIndex < DisplayabilityBase.StartDisplayIndex)
                found.DisplayIndex = info.DisplayIndex;

            //Replace with the new Icon.
            if (info.Icon != null)
                found.Icon = info.Icon;

            //Add items into the found
            found.Items.AddRange(info.Items);

            ConsolidateSeparetorInfo(found);
        }

        private static void ConsolidateSeparetorInfo(MenuInfo item)
        {
            //Remove the first item of Found if it is Separetor
            while (item.Items.Count > 0)
            {
                var found = false;
                //Remove the SeparetorInfo on top.
                if (item.Items[0] is SeparetorInfo)
                {
                    item.Items.RemoveAt(0);
                    found = true;
                }

                //Remove the SeparetorInfo at the buttom.
                if (item.Items.Count > 0 && item.Items[item.Items.Count - 1] is SeparetorInfo)
                {
                    item.Items.RemoveAt(item.Items.Count - 1);
                    found = true;
                }

                if (!found) break;
            }
        }
    }
}