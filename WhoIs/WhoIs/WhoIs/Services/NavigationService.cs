using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Services.Interface;
using WhoIs.ViewModels;
using WhoIs.Views;
using Xamarin.Forms;

namespace WhoIs.Services
{
    public class NavigationService : INavigationService
    {
        public BaseViewModel PreviousPageViewModel => throw new NotImplementedException();

        public Task InitializeAsync()
        {
            return NavigateToAsync<LoginViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveBackStackAsync()
        {

            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                return navigationPage.PopToRootAsync();
            }

            return null;
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var navigationPage = Application.Current.MainPage as CustomNavigationView;
            if (navigationPage != null)
            {
                return navigationPage.PopAsync();
            }

            return null;
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType, parameter);
            page.BindingContext = DependencyContainer.Container.Resolve(viewModelType, null);

            if (viewModelType.Equals(typeof(LoginViewModel)))
            {
                bool isUserLogged = await (page.BindingContext as BaseViewModel).isUserLogged();
                if (isUserLogged)
                {
                    viewModelType = typeof(HomeViewModel);
                    page = CreatePage(viewModelType, parameter);
                    Application.Current.MainPage = new CustomNavigationView(page);
                }

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
           
            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
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
