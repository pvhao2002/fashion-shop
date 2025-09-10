using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChoTotAsp.Entity
{
    public partial class product_variants
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product_variants()
        {
            cart_items = new HashSet<cart_items>();
            order_items = new HashSet<order_items>();
        }

        [Key]
        public int variant_id { get; set; }

        public int? product_id { get; set; }

        [StringLength(10)]
        public string size { get; set; }

        [StringLength(50)]
        public string color { get; set; }

        public decimal? additional_price { get; set; }

        public int? stock { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_items> cart_items { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_items> order_items { get; set; }

        public virtual product product { get; set; }
    }
}
