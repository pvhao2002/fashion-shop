using System.Collections;
using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.User.ViewModel
{
    public class NewViewModel
    {
        public ICollection<product> ListPosts { get; set; } = new List<product>();

        public ICollection<category> ListCategory { get; set; } = new List<category>();

        public ICollection<product> ListNews { get; set; } = new List<product>();
        public int TotalNews { get; set; }

        // filters
        public string ViewMode { get; set; }
        public string SortBy { get; set; }
        public string SortType { get; set; }
        public string Sort { get; set; }
        public int CategoryId { get; set; }
        public int ProvinceId { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }


        // pagination
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalPage { get; set; }

        // link of pagination
        public string NextPageLink { get; set; }
        public string PrevPageLink { get; set; }
    }
}