using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.Admin.ViewModel
{
    public class ProductViewModel
    {
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<product> Products { get; set; }
        public string Status { get; set; }
        
        public string SearchTerm { get; set; }
    }
}
