using System.ComponentModel.DataAnnotations;

namespace Project.Entity
{
    public partial class cart_items
    {
        [Key] public int cart_item_id { get; set; }

        public int? cart_id { get; set; }

        public int? product_id { get; set; }


        public int? quantity { get; set; }

        public decimal? total_item_price { get; set; }

        public virtual cart cart { get; set; }

        public virtual product product { get; set; }
    }
}