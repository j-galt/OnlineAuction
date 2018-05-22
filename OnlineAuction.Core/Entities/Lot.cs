using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnlineAuction.Core.Entities
{
    public class Lot
    {
        public Lot()
        {
            Bids = new Collection<Bid>();
        }

        public int LotID { get; set; }
        public string LotName { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string AppUserID { get; set; }
        public decimal StartPrice { get; set; }
        public decimal MinBid { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int LotStateID { get; set; }

        public ICollection<Bid> Bids { get; set; }
        public LotState LotState { get; set; }        
    }
}
