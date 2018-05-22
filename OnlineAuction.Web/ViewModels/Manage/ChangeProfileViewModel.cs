using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineAuction.Web.ViewModels.Manage
{
    public class ChangeProfileViewModel
    {
        public ChangeProfileViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }

        public string UserID { get; set; }
        public string Email { get; set; }
        public IEnumerable<IdentityRole> AllRoles { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
