using System;
using System.Collections.Generic;

namespace OnlineAuction.Web.ViewModels.Lot
{
    public class LotDetailsViewModel
    {
        public LotDetailsViewModel()
        {
            Bids = new List<BidViewModel>();
        }

        public int LotID { get; set; }
        public string LotName { get; set; }
        public string Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public string AppUserID { get; set; }
        public decimal StartPrice { get; set; }
        public decimal MinBid { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalBids { get; set; }
        public IEnumerable<BidViewModel> Bids { get; set; }
    }
}
