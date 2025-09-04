namespace ChoTotAsp.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cart_items
    {
        [Key]
        public int cart_item_id { get; set; }

        public int? cart_id { get; set; }

        public int? product_id { get; set; }

        public int? variant_id { get; set; }

        public int? quantity { get; set; }

        public decimal? price { get; set; }

        public virtual cart cart { get; set; }

        public virtual product product { get; set; }

        public virtual product_variants product_variants { get; set; }
    }
}
