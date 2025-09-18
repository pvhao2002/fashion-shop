using System.Collections.Generic;
using Project.Entity;

namespace Project.Areas.Admin.ViewModel
{
    public class UserManagementViewModel
    {
        public string Title { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<user> Accounts { get; set; }
        public string SearchTerm { get; set; }
        
        public ICollection<UserStatistic> ListUserStatistic { get; set; }
    }
}
