using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Managers.Interface;
using WhoIs.Services.Interface;
using Unity;

namespace WhoIs.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService _navigationService;

        protected IAppUserManager _appUserManager;

        public BaseViewModel()
        {
            _navigationService = DependencyContainer.Container.Resolve<INavigationService>();
            _appUserManager = DependencyContainer.Container.Resolve<IAppUserManager>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual async Task InitializeAsync(object navigationData)
        {
            await Task.Run(null);
        }

        public async Task<bool> isUserLogged()
        {
            return await _appUserManager.GetLoggedUser() != null;
        }
    }
}
