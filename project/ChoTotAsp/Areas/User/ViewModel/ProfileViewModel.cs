using System.Collections.Generic;
using ChoTotAsp.Entity;

namespace ChoTotAsp.Areas.User.ViewModel
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