using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.User.ViewModel
{
    public class DetailNewsViewModel
    {
        public product CurrentProduct { get; set; }
        public ICollection<product> RelatedProduct { get; set; } = new List<product>();
    }
}