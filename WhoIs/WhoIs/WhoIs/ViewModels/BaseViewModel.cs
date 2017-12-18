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
using System.Linq.Expressions;

namespace WhoIs.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService _navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        protected bool _isInitialized;
        public bool IsInitialized { get { return _isInitialized; } set { SetPropertyValue(ref _isInitialized, value); } }

        protected bool _isLoading;
        public bool IsLoading { get { return _isLoading; } set { SetPropertyValue(ref _isLoading, value); } }

        protected bool _isEnabled;
        public bool IsEnabled { get { return _isEnabled; } set { SetPropertyValue(ref _isEnabled, value); } }

        public delegate Task<bool> AlertDelegate(string title, string message, string accept, string cancel);
        public AlertDelegate Alert { get; set; }
        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Alert(title, message, accept, cancel);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetPropertyValue<T>(ref T storageField, T newValue, Expression<Func<T>> propExpr)
        {
            if (Equals(storageField, newValue))
            {
                return false;
            }

            storageField = newValue;
            var prop = (System.Reflection.PropertyInfo)((MemberExpression)propExpr.Body).Member;
            this.RaisePropertyChanged(prop.Name);

            return true;
        }

        protected bool SetPropertyValue<T>(ref T storageField, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(storageField, newValue))
            {
                return false;
            }

            storageField = newValue;
            this.RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaiseAllPropertiesChanged()
        {
            // By convention, an empty string indicates all properties are invalid.
            if(this.PropertyChanged!=null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propExpr)
        {
            var prop = (System.Reflection.PropertyInfo)((MemberExpression)propExpr.Body).Member;
            this.RaisePropertyChanged(prop.Name);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task Refresh()
        {
            return Task.FromResult(false);
        }
    }
}
