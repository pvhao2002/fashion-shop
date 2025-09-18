using System;
using System.Collections.Generic;
using Project.Entity;

namespace Project.Areas.Admin.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<ProductImageDTO> Images { get; set; }
        public CategoryDTO Category { get; set; }
        public ProductDTO() { }
        public ProductDTO(product p)
        {
            ProductId = p.product_id;
            CategoryId = p.category_id;
            Name = p.name;
            Description = p.description;
            Price = p.price;
            Stock = p.stock;
            Status = p.status;
            CreatedAt = p.created_at;
            UpdatedAt = p.updated_at;
            Images = new List<ProductImageDTO>();
            if (p.category != null)
            {
                Category = new CategoryDTO
                {
                    CategoryId = p.category.category_id,
                    CategoryName = p.category.category_name,
                    Image = p.category.image,
                    status = p.category.status,
                    CreatedAt = p.category.created_at,
                    UpdatedAt = p.category.updated_at
                };
            }

            if (p.product_images == null) return;
            foreach (var img in p.product_images)
            {
                Images.Add(new ProductImageDTO
                {
                    ImageId = img.image_id,
                    ProductId = img.product_id,
                    ImageUrl = img.image_url,
                    IsMain = img.is_main
                });
            }
        }
    }
}