using System.Collections.Generic;
using Project.Entity;

namespace Project.Areas.User.ViewModel
{
    public class ProfileViewModel
    {
        public user CurrentUser { get; set; }
        public int TotalPost { get; set; }
        public int TotalWishList { get; set; }
        
        public int RoomId { get; set; }
        public int CurrentUserId { get; set; }
    }
}