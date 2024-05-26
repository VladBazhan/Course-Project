using Microsoft.AspNetCore.Identity;
using ProjectCinema.Enums;

namespace ProjectCinema.Models
{
    public class User : IdentityUser
    {
        public string Name {  get; set; }
        public Roles Role { get; set; } = 0;
    }
}
