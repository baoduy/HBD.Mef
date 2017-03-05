#region

using HBD.Framework.Collections;
using HBD.Mef.Core;
using System.Collections.Specialized;

#endregion

namespace HBD.Mef.Shell.Navigation
{
    public class MenuInfoCollection : ObservableSortedCollection<int, IMenuInfo>, IMenuInfoCollection
    {
        public MenuInfoCollection() : base(i => i.DisplayIndex)
        {
        }

        public virtual IMenuInfo this[string titleOrName] => this.GetItemByTitleOrName(titleOrName);

        public override void Add(IMenuInfo item)
        {
            if (item.DisplayIndex <= 0)
                item.DisplayIndex = this.Count;

            base.Add(item);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (e.Action != NotifyCollectionChangedAction.Add) return;

            //Update the display index according with position index in the list
            this.StopRaisingEvent = true;

            for (var i = 0; i < this.Count; i++)
                this[i].DisplayIndex = i;

            this.StopRaisingEvent = false;
        }
    }
}