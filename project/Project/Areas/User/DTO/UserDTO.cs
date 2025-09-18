using Project.Entity;

namespace Project.Areas.User.DTO
{
    public class UserDTO
    {
        public int userId { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }

        public UserDTO()
        {
        }

        public UserDTO(user u)
        {
            this.userId = u.user_id;
            this.email = u.email;
            this.fullName = u.full_name;
        }
    }
}