namespace ChoTotAsp.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
