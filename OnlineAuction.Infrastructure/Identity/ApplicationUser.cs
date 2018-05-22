using Microsoft.AspNetCore.Identity;
using OnlineAuction.Core.Entities;
using System.Collections.Generic;

namespace OnlineAuction.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Bid> Bids { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
