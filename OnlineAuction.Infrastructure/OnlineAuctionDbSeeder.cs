using OnlineAuction.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineAuction.Infrastructure
{
    public class OnlineAuctionDbSeeder
    {
        public static async Task SeedAsync(OnlineAuctionDbContext context)
        {
            if (!context.Lots.Any())
            {
                context.Lots.AddRange(new List<Lot>
                {
                    new Lot { CategoryID = 1, Description = "Skirt", LotName = "Skirt", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7) },
                    new Lot { CategoryID = 1, Description = "Trousers", LotName = "Trousers", MinBid = 100, StartPrice = 100, LotStateID = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(7) },
                    new Lot { CategoryID = 1, Description = "Socks", LotName = "Socks", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(4) },
                    new Lot { CategoryID = 1, Description = "Gloves", LotName = "Gloves", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(4) },
                    new Lot { CategoryID = 1, Description = "Jacket", LotName = "Jacket", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 1, Description = "Shorts", LotName = "Shorts", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(3) },
                    new Lot { CategoryID = 1, Description = "Magic Socks", LotName = "Magic Socks", MinBid = 1000, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(8) },
                    new Lot { CategoryID = 1, Description = "Retro scarf", LotName = "Retro scarf", MinBid = 1, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(5) },
                    new Lot { CategoryID = 2, Description = "First To Die", LotName = "First To Die", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(5) },
                    new Lot { CategoryID = 2, Description = "English Grammar In Use", LotName = "English Grammar In Use", MinBid = 133, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 2, Description = "English Vocabulary In Use", LotName = "English Vocabulary In Use", MinBid = 2, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 2, Description = "1984", LotName = "1984", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 2, Description = "CLR Via C#", LotName = "CLR Via C#", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 2, Description = "Pro Linq", LotName = "Pro Linq", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(21) },
                    new Lot { CategoryID = 2, Description = "Harry Potter And The Sorcerer's Stone", LotName = "Harry Potter And The Sorcerer's Stone", MinBid = 367, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 3, Description = "Table", LotName = "Table", MinBid = 1, StartPrice = 1, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 3, Description = "Statuette", LotName = "Statuette", MinBid = 3367, StartPrice = 1, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 3, Description = "Chair", LotName = "Chair", MinBid = 367, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                    new Lot { CategoryID = 4, Description = "asdasd", LotName = "Gold Ring", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(26) },
                    new Lot { CategoryID = 4, Description = "asdasd", LotName = "Earrings", MinBid = 12, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(3) },
                    new Lot { CategoryID = 4, Description = "asdasd", LotName = "Very Nice Bracelet", MinBid = 7, StartPrice = 100, LotStateID = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) },
                });

                await context.SaveChangesAsync();
            }            
        }
    }
}
