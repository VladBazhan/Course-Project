using ProjectCinema.Enums;

namespace ProjectCinema.Models
{
    public class ModelAccount
    {
        public string Id { get; set; } = "";
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Roles Role { get; set; }
        public string Password { get; set; }
    }
}
