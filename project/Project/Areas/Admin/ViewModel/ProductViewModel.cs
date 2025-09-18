using System.Collections.Generic;
using System.Web.Mvc;
using Project.Areas.Admin.DTO;
using Project.Entity;

namespace Project.Areas.Admin.ViewModel
{
    public class ProductViewModel
    {
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public product ProductModel { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public string Status { get; set; }

        public string SearchTerm { get; set; }
    }
}