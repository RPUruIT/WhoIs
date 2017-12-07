using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.ViewModels.Helper;
using WhoIs.Managers.Interface;
using WhoIs.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace WhoIs.ViewModels
{
    public class UserHuntedDetailsViewModel:BaseViewModel
    {
        IUserToHuntManager _userToHuntManager;

        private bool _seeDetails;
        public bool SeeDetails
        {
            get { return _seeDetails; }
            set { SetPropertyValue(ref _seeDetails, value); }
        }

        private bool _showConfirmButton;
        public bool ShowConfirmButton
        {
            get { return _showConfirmButton; }
            set { SetPropertyValue(ref _showConfirmButton, value); }
        }

        private bool _enableComments;
        public bool EnableComments
        {
            get { return _enableComments; }
            set { SetPropertyValue(ref _enableComments, value); }
        }

        public string Comments { get; } = "Comentarios";
        public string Confirm { get; } = "Confirmar";

        private UserToHunt _userHunted;
        public UserToHunt UserHunted
        {
            get { return _userHunted; }
            set { SetPropertyValue(ref _userHunted, value); }
        }

        private ICommand _cmdConfirm;
        public ICommand CmdConfirm
        {
            get { return _cmdConfirm; }
            set { SetPropertyValue(ref _cmdConfirm, value); }
        }

        public UserHuntedDetailsViewModel(IUserToHuntManager userToHuntManager) {
            _userToHuntManager = userToHuntManager;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            UserHuntedDetailsViewParameter param = navigationData as UserHuntedDetailsViewParameter;
            UserHunted = param.UserToHunt;
            SeeDetails = param.SeeDetails;

            CmdConfirm = new Command(async () => await ConfirmUserHunted());
            ShowConfirmButton = !SeeDetails;
            EnableComments = !SeeDetails;
        }

        public async Task ConfirmUserHunted()
        {
            await _userToHuntManager.HuntUser(_userHunted);
            await _navigationService.PopAsync();
        }
    }
}
