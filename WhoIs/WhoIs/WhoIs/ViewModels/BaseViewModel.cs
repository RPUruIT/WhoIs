using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WhoIs.Services.Interface;

namespace WhoIs.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
    }
}
