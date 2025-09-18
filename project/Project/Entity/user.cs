using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Project.Entity
{
    public class user
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            carts = new HashSet<cart>();
            orders = new HashSet<orders>();
            product_comments = new HashSet<product_comments>();
            reviews = new HashSet<review>();
        }

        [Key] public int user_id { get; set; }

        [StringLength(100)] public string full_name { get; set; }

        [StringLength(100)] public string email { get; set; }

        [StringLength(255)] public string password_hash { get; set; }

        [StringLength(20)] public string phone { get; set; }

        public string address { get; set; }
        public string avatar { get; set; }

        [StringLength(20)] public string status { get; set; }

        [StringLength(20)] public string role { get; set; }

        public DateTime? created_at { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_comments> product_comments { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<review> reviews { get; set; }
    }
}