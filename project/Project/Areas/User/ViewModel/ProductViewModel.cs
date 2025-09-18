using System.Collections.Generic;
using Project.Areas.Admin.DTO;
using Project.Entity;

namespace Project.Areas.User.ViewModel
{
    public class ProductViewModel
    {
        public int TotalProduct { get; set; }
        public List<category> ListCategory { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<ProductDTO> RelativeProduct { get; set; }
        public ProductDTO Product { get; set; }
        
    }
}