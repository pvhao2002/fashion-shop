using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChoTotAsp.Entity
{
    public class category
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public category()
        {
            products = new HashSet<product>();
        }

        [Key]
        public int category_id { get; set; }

        [StringLength(100)]
        public string category_name { get; set; }

        public string image { get; set; }

        [StringLength(20)]
        public string status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
