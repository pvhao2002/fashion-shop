namespace Project.Areas.Admin.DTO
{
    public class ProductImageDTO
    {
        public int ImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsMain { get; set; }
    }
}