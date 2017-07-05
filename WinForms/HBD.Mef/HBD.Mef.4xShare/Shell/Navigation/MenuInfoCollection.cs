#region using

using HBD.Framework.Collections;
using System.Diagnostics;
using System.Linq;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    [DebuggerDisplay("Count = {Count}")]
    public class MenuInfoCollection : ObservableSortedCollection<int, IMenuInfo>, IMenuInfoCollection
    {
        public MenuInfoCollection() : base(i => i.DisplayIndex)
        {
        }

        public virtual IMenuInfo this[string titleOrName] => this.GetItemByTitleOrName(titleOrName);

        public override void Add(IMenuInfo item)
        {
            if (item.DisplayIndex <= 0)
            {
                item.DisplayIndex = Count;
                item.PropertyChanged += Item_PropertyChanged;
            }

            base.Add(item);
        }
        public override bool Remove(IMenuInfo item)
        {
            item.PropertyChanged -= Item_PropertyChanged;
            return base.Remove(item);
        }

        public override void Clear()
        {
            foreach (var item in this)
                item.PropertyChanged -= Item_PropertyChanged;

            base.Clear();
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var m = sender as IMenuInfo;
            if (m == null) return;

            var m1 = this.FirstOrDefault(i => i != m && i.DisplayIndex == m.DisplayIndex);
            if (m1 == null) return;

            var m0 = this.FirstOrDefault(i => i.DisplayIndex == m1.DisplayIndex - 1);

            if (m0 == null && m1.DisplayIndex > 0)
                m1.DisplayIndex -= 1;
            else m1.DisplayIndex += 1;
        }
    }
}