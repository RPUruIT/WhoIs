using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WhoIs.CustomComponents
{
    public class CustomListView:ListView
    {

        public CustomListView():base(ListViewCachingStrategy.RecycleElement)
        {
            
            this.ItemSelected += CustomListView_ItemSelected; 
            
        }

        private void CustomListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //this.SelectedItem = null;
        }
    }
}
