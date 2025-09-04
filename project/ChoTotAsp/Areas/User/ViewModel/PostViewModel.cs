using System.Collections.Generic;
using System.Web.Mvc;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.User.ViewModel
{
    public class PostViewModel
    {
        public ICollection<category> ListCategory { get; set; } = new List<category>();

        public ICollection<SelectListItem> DropdownProvinces { get; set; } = new List<SelectListItem>();
        public ICollection<SelectListItem> DropdownCategory { get; set; } = new List<SelectListItem>();

        public product Post { get; set; } = new product();
    }
}