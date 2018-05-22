using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OnlineAuction.Core.Entities
{
    public class Category
    {
        public Category()
        {
            SubCategories = new Collection<Category>();
            Lots = new Collection<Lot>();
        }

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }

        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
