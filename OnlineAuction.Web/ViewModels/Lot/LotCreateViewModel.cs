using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Web.ViewModels.Lot
{
    public class LotCreateViewModel
    {
        public int LotID { get; set; }

        [Required]
        public string LotName { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryID { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        public decimal StartPrice { get; set; }

        [Required]
        public decimal MinBid { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
