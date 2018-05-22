using System;

namespace OnlineAuction.Web.ViewModels.Lot
{
    public class BidViewModel
    {
        public int BidID { get; set; }
        public string AppUserID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
