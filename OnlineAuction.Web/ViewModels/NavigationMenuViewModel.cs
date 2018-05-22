using System.Collections.Generic;

namespace OnlineAuction.Web.ViewModels
{
    public class NavigationMenuViewModel
    {
        public NavigationMenuViewModel()
        {
            Categories = new List<CategoryViewModel>();
        }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public object CurrentCategory { get; set; }
    }
}
