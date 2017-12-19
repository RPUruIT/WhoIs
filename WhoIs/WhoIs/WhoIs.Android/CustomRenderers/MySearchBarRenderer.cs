using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WhoIs.CustomRenderes;
using WhoIs.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MySearchBar), typeof(MySearchBarRenderer))]
namespace WhoIs.Droid.CustomRenderers
{
    public class MySearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var searchView = Control;
                searchView.Iconified = true;
                searchView.SetIconifiedByDefault(false);

                int searchViewButtonId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
                var searchIcon = searchView.FindViewById(searchViewButtonId);
                (searchIcon as ImageView).SetImageResource(Resource.Drawable.ic_leftarrow);

            }
        }
    }
}