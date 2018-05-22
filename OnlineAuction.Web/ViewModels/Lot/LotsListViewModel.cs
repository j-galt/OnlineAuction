using System.Collections.Generic;

namespace OnlineAuction.Web.ViewModels.Lot
{
    public class LotsListViewModel
    {
        public IEnumerable<LotSummaryViewModel> Lots { get; set; }
        public PagingInfoViewModel PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
