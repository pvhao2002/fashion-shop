using System.ComponentModel.DataAnnotations;

namespace Project.Entity
{
    public partial class order_items
    {
        [Key]
        public int order_item_id { get; set; }

        public int? order_id { get; set; }

        public int? product_id { get; set; }

        public int? quantity { get; set; }

        public decimal? price { get; set; }

        public virtual orders Orders { get; set; }

        public virtual product product { get; set; }

    }
}
