using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project.Entity
{
    [Table("orders")]
    public partial class orders
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public orders()
        {
            order_items = new HashSet<order_items>();
        }

        [Key] public int order_id { get; set; }

        public int? user_id { get; set; }

        [StringLength(20)] public string order_status { get; set; }

        [StringLength(20)] public string payment_status { get; set; }

        public decimal? total_amount { get; set; }
        [StringLength(15)] public string phone_number { get; set; }

        public string shipping_address { get; set; }
        public string note { get; set; }
        public string payment_method { get; set; }
        public string discount_code { get; set; }
        public string discount_amount { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_items> order_items { get; set; }

        public virtual user user { get; set; }
    }
}