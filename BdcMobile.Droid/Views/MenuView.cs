using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using BdcMobile.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace BdcMobile.Droid.Views
{
    [MvxFragmentPresentation(typeof(EventListViewModel), Resource.Id.navigation_frame)]
    [Register(nameof(MenuView))]
    public class MenuView : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuView, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);

            var iconPlanets = _navigationView.Menu.FindItem(Resource.Id.nav_settings);
            var imgPlanet = VectorDrawableCompat.Create(Resources, Resource.Drawable.settings, Activity.Theme);
            iconPlanets.SetIcon(imgPlanet);

            var iconPeople = _navigationView.Menu.FindItem(Resource.Id.nav_debug);
            var imgPeople = VectorDrawableCompat.Create(Resources, Resource.Drawable.debug, Activity.Theme);
            iconPeople.SetIcon(imgPeople);

            var iconStatistics = _navigationView.Menu.FindItem(Resource.Id.nav_logout);
            var imgStatistics = VectorDrawableCompat.Create(Resources, Resource.Drawable.sign_out, Activity.Theme);
            iconStatistics.SetIcon(imgStatistics);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Task.Run(() => Navigate(item.ItemId));
            return true;
        }

        private async Task Navigate(int itemId)
        {
            switch (itemId)
            {
                case Resource.Id.nav_settings:
                    ViewModel.ShowSettingsCommand.Execute(null);
                    break;
                case Resource.Id.nav_debug:
                    ViewModel.ShowDebugCommand.Execute(null);
                    break;
                case Resource.Id.nav_logout:
                    await ViewModel.DoLogoutCommand.ExecuteAsync(null);
                    break;
            }

            ((EventListView)Activity).DrawerLayout.CloseDrawers();
        }
    }
}