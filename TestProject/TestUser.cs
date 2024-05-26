using ProjectCinema.Models;
using ProjectCinema.Enums;
using System.Net.Sockets;
namespace TestProject
{
    public class TestUser
    {
        [Fact]
        public void Usertest()
        {
            User user = new()
            {
                Name = "User",
                Role = Roles.User,
                Id = "lqjsdafh27fh2ehfq9wf7",
                PhoneNumber = "1234567890",
                Email = "user@gmail.com",
                UserName = "user",
            };
            Assert.Equal("User", user.Name);
            Assert.Equal(Roles.User, user.Role);
            Assert.Equal("lqjsdafh27fh2ehfq9wf7", user.Id);
            Assert.Equal("1234567890", user.PhoneNumber);
            Assert.Equal("user@gmail.com", user.Email);
            Assert.Equal("user", user.UserName);
        }
    }
}