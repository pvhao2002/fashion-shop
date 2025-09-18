using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Entity
{
    public partial class product_comments
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product_comments()
        {
            product_comments1 = new HashSet<product_comments>();
        }

        [Key]
        public int comment_id { get; set; }

        public int? product_id { get; set; }

        public int? user_id { get; set; }

        public int? parent_id { get; set; }

        public string content { get; set; }

        public DateTime? created_at { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_comments> product_comments1 { get; set; }

        public virtual product_comments product_comments2 { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
