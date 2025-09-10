using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChoTotAsp.Entity
{
    public partial class product
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            cart_items = new HashSet<cart_items>();
            order_items = new HashSet<order_items>();
            product_comments = new HashSet<product_comments>();
            product_images = new HashSet<product_images>();
            product_variants = new HashSet<product_variants>();
            reviews = new HashSet<review>();
        }

        [Key]
        public int product_id { get; set; }

        public int? category_id { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        public string description { get; set; }

        public decimal? price { get; set; }

        public int? stock { get; set; }

        [StringLength(20)]
        public string status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_items> cart_items { get; set; }

        public virtual category category { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_items> order_items { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_comments> product_comments { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_images> product_images { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_variants> product_variants { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<review> reviews { get; set; }
    }
}
