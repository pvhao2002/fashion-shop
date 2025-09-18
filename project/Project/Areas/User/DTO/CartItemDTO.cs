using Project.Areas.Admin.DTO;
using Project.Entity;

namespace Project.Areas.User.DTO
{
    public class CartItemDTO
    {
        public int cartItemId { get; set; }

        public ProductDTO Product { get; set; }

        public int? quantity { get; set; }

        public decimal? totalItemPrice { get; set; }

        public CartItemDTO()
        {
        }

        public CartItemDTO(cart_items ci)
        {
            this.cartItemId = ci.cart_item_id;
            this.quantity = ci.quantity;
            this.totalItemPrice = ci.total_item_price;
            if (ci.product != null)
            {
                this.Product = new ProductDTO(ci.product);
            }
        }
    }
}