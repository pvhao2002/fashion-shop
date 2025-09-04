
namespace ChoTotAsp.Areas.Admin.ViewModel
{
    public class DashboardViewModel
    {
        public string Title { get; set; }
        public int TotalUsers { get; set; }
        public int TotalMessages { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalProductPending { get; set; }
        public int TotalProductApproved { get; set; }
    }
}