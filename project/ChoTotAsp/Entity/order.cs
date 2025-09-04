namespace ChoTotAsp.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            order_items = new HashSet<order_items>();
        }

        [Key]
        public int order_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(20)]
        public string order_status { get; set; }

        [StringLength(20)]
        public string payment_status { get; set; }

        public decimal? total_amount { get; set; }

        public string shipping_address { get; set; }

        public DateTime? created_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_items> order_items { get; set; }

        public virtual user user { get; set; }
    }
}
