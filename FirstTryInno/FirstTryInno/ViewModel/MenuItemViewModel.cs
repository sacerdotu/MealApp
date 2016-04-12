using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel
{
    public class MenuItemViewModel : NotificationBase<ProviderMenuItem>
    {
        public MenuItemViewModel(ProviderMenuItem menuItem = null) 
            : base(menuItem)
        {
        }

        public void Reset(ProviderMenuItem newObject)
        {
            if (newObject == null)
                return;

            Id = newObject.ProviderMenuItemID;
            Name = newObject.Name;
            LikeCount = newObject.LikeCount;
            DislikeCount = newObject.DislikeCount;
        }

        public String Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

        public int LikeCount
        {
            get { return This.LikeCount; }
            set { SetProperty(This.LikeCount, value, () => This.LikeCount = value); }
        }

        public int DislikeCount
        {
            get { return This.DislikeCount; }
            set { SetProperty(This.DislikeCount, value, () => This.DislikeCount = value); }
        }

        public long Id
        {
            get { return This.ProviderMenuItemID; }
            set { SetProperty(This.ProviderMenuItemID, value, () => This.ProviderMenuItemID = value); }
        }
    }
}
