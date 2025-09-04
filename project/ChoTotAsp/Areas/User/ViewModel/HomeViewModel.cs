using System.Collections;
using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.User.ViewModel
{
    public class HomeViewModel
    {
        public ICollection<category> Categories { get; set; } = new List<category>();
        public ICollection<product> ProductList { get; set; } = new List<product>();
    }
}