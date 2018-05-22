using System;

namespace OnlineAuction.Core.Entities
{
    public class Bid
    {
        public int BidID { get; set; }
        public int LotID { get; set; }
        public string AppUserID { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
    }
}
