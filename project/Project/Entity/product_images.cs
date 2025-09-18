using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entity
{
    public partial class product_images
    {
        [Key] public int image_id { get; set; }

        public int product_id { get; set; }

        [Column(TypeName = "nvarchar(max)")] public string image_url { get; set; }

        public bool? is_main { get; set; }

        public virtual product product { get; set; }
    }
}