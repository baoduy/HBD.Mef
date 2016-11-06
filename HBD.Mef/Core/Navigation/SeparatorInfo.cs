using System.ComponentModel;

namespace HBD.Mef.Core.Navigation
{
    public sealed class SeparatorInfo : IMenuInfo
    {
        public SeparatorInfo(IMenuInfoCollection parent)
        {
            Parent = parent;
        }

        public MenuAlignment Alignment { get; set; } = MenuAlignment.Left;
        public IMenuInfoCollection Parent { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}