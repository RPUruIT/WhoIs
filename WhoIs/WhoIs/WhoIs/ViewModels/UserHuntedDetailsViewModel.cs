using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoIs.ViewModels.Helper;
using WhoIs.Managers.Interface;
using WhoIs.Models;

namespace WhoIs.ViewModels
{
    public class UserHuntedDetailsViewModel:BaseViewModel
    {
        IUserToHuntManager _userToHuntManager;

        private string _userHuntedImage;
        public string UserHuntedImage
        {
            get { return _userHuntedImage; }
            set { SetPropertyValue(ref _userHuntedImage, value); }
        }

        private bool _seeDetails;
        public bool SeeDetails
        {
            get { return _seeDetails; }
            set { SetPropertyValue(ref _seeDetails, value); }
        }

        public UserHuntedDetailsViewModel(IUserToHuntManager userToHuntManager) {
            _userToHuntManager = userToHuntManager;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await Task.Delay(1);
            UserHuntedDetailsViewParameter param = navigationData as UserHuntedDetailsViewParameter;
            UserToHunt userToHunt = param.UserToHunt;
            bool seeDetails = param.SeeDetails;

            UserHuntedImage = userToHunt.ImgPath;
            SeeDetails = seeDetails;
        }

    }
}
