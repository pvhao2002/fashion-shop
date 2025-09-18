using System.Collections.Generic;

namespace Project.Areas.Admin.Payload
{
    public class ProductPayload
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        
        public List<string> Images { get; set; }
    }
}