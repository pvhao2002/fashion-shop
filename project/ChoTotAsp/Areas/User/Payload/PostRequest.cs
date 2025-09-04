using System.Collections.Generic;

namespace ChoTotAsp.Areas.User.Payload
{
    public class PostRequest
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int ProvinceId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> ImageUrls { get; set; }
    }
}