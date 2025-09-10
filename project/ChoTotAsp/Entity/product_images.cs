using System.ComponentModel.DataAnnotations;

namespace ChoTotAsp.Entity
{
    public partial class product_images
    {
        [Key]
        public int image_id { get; set; }

        public int product_id { get; set; }

        [Required]
        [StringLength(255)]
        public string image_url { get; set; }

        public bool? is_main { get; set; }

        public virtual product product { get; set; }
    }
}
