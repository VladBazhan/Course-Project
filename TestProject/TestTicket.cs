using ProjectCinema.Models;

namespace TestProject
{
    public class TestTicket
    {
        [Fact]
        public void Ticket()
        {
            Ticket ticket = new()
            {
                Id = 1,
                FilmName = "Deadpool 3",
                Price = 80,
                SenderId = "lqjsdafh27fh2ehfq9wf7",
                Name = "User",
                Email = "user@gmail.com",
                Time = "2024-05-26T20:40"
            };
            Assert.Equal(1, ticket.Id);
            Assert.Equal("Deadpool 3", ticket.FilmName);
            Assert.Equal(80, ticket.Price);
            Assert.Equal("lqjsdafh27fh2ehfq9wf7", ticket.SenderId);
            Assert.Equal("User", ticket.Name);
            Assert.Equal("user@gmail.com", ticket.Email);
            Assert.Equal("2024-05-26T20:40", ticket.Time);
        }
    }
}