using System;
using System.Collections.Generic;
using System.Linq;
using Project.Entity;

namespace Project.Areas.User.DTO
{
    public class CartDTO
    {
        public int cartId { get; set; }

        public int? userId { get; set; }
        public decimal totalPrice { get; set; }
        public int totalItems { get; set; }

        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set; }

        public List<CartItemDTO> cartItems { get; set; }

        public CartDTO()
        {
            cartItems = new List<CartItemDTO>();
        }

        public CartDTO(cart c)
        {
            this.cartItems = new List<CartItemDTO>();
            if (c == null) return;
            this.cartId = c.cart_id;
            this.userId = c.user_id;
            this.totalPrice = c.total_price;
            this.totalItems = c.total_items;
            this.createdAt = c.created_at;
            this.updatedAt = c.updated_at;
            
            if (c.cart_items != null)
            {
                this.cartItems = c.cart_items.Select(it => new CartItemDTO(it)).ToList();
            }
        }
    }
}