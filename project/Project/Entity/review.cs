using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Entity
{
    public partial class review
    {
        [Key]
        public int review_id { get; set; }

        public int? product_id { get; set; }

        public int? user_id { get; set; }

        public int? rating { get; set; }

        public string comment { get; set; }

        public DateTime? created_at { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
