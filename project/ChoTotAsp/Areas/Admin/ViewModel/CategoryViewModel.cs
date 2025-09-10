using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.Admin.ViewModel
{
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public ICollection<category> Categories { get; set; }

        public string SearchTerm { get; set; }
    }
}