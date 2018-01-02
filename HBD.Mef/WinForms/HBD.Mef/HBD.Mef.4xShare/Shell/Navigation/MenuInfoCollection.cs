#region using

using HBD.Framework.Collections;
using System.Diagnostics;
using System.Linq;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    [DebuggerDisplay("Count = {" + nameof(Count) + "}")]
    public class MenuInfoCollection : ObservableSortedCollection<IMenuInfo>, IMenuInfoCollection
    {
        public MenuInfoCollection() : base(i => i.DisplayIndex)
        {
        }

        public virtual IMenuInfo this[string titleOrName] => this.GetItemByTitleOrName(titleOrName);

        protected override void InsertItem(int index, IMenuInfo item)
        {
            if (item.DisplayIndex <= 0)
                item.DisplayIndex = this.Count + 1;

            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, IMenuInfo item)
        {
            if (item.DisplayIndex <= 0)
                item.DisplayIndex = this.Count + 1;

            base.SetItem(index, item);
        }
    }
}