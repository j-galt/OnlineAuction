using System;

namespace OnlineAuction.Web.ViewModels.Lot
{
    public class LotSummaryViewModel
    {
        public int LotID { get; set; }
        public string Name { get; set; }
        public DateTime EndTime { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
