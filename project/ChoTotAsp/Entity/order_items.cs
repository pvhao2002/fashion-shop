using System.ComponentModel.DataAnnotations;

namespace ChoTotAsp.Entity
{
    public partial class order_items
    {
        [Key]
        public int order_item_id { get; set; }

        public int? order_id { get; set; }

        public int? product_id { get; set; }

        public int? variant_id { get; set; }

        public int? quantity { get; set; }

        public decimal? price { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }

        public virtual product_variants product_variants { get; set; }
    }
}
