using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using WhoIs.Services.Interface;
using WhoIs.ViewModels;
using WhoIs.Views;
using Xamarin.Forms;
using Unity;

namespace WhoIs.Services
{
    public class NavigationService : INavigationService
    {
        public async Task InitializeAsync()
        {
            try { 
                IAppUserManager appUserManager = DependencyContainer.Container.Resolve<IAppUserManager>();

                bool isUserLoged = await appUserManager.IsUserLogged();
                if (isUserLoged)
                {
                    await NavigateToAsync<HomeViewModel>();
                }
                else
                    await NavigateToAsync<LoginViewModel>();
            }
            catch(Exception ex)
            {

            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public async Task PopAsync()
        {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            await navigationPage.PopAsync();
            await (navigationPage.CurrentPage.BindingContext as BaseViewModel).Refresh();
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
            object viewModel = DependencyContainer.Container.Resolve(viewModelType, null);

            if (viewModelType.Equals(typeof(LoginViewModel)) || viewModelType.Equals(typeof(HomeViewModel)))
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
            else
            {
                var navigationPage = Application.Current.MainPage as CustomNavigationView;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    Application.Current.MainPage = new CustomNavigationView(page);
                }
            }

            page.BindingContext = viewModel;
            BaseViewModel baseViewModel = (page.BindingContext as BaseViewModel);
            baseViewModel.Alert = page.DisplayAlert;
            if (!baseViewModel.IsInitialized)
                await baseViewModel.InitializeAsync(parameter);
            else
                await baseViewModel.Refresh();
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(
                        CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }

    }
}
