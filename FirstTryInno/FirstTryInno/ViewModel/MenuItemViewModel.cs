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

        public String Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }
        }

        public long Id
        {
            get { return This.ProviderMenuItemID; }
            set { SetProperty(This.ProviderMenuItemID, value, () => This.ProviderMenuItemID = value); }
        }
    }
}
