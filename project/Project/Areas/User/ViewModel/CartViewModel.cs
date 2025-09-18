using System.Collections.Generic;
using Project.Areas.User.DTO;
using Project.Entity;

namespace Project.Areas.User.ViewModel
{
    public class CartViewModel
    {
        public UserDTO User { get; set; }
        public CartDTO Cart { get; set; }
        public List<cart> Carts { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }
    }
}