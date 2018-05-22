using Microsoft.AspNetCore.Identity;
using OnlineAuction.Core.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnlineAuction.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Bids = new Collection<Bid>();
            Lots = new Collection<Lot>();
        }

        public ICollection<Bid> Bids { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
