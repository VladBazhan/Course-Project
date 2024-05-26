using ProjectCinema.Models;
using System;

namespace TestProject
{
    public class TestFilm
    {
        [Fact]
        public void Film()
        {
            Sessions session = new()
            {
                Id = 1,
                Date = "2024-05-26T20:40",
                FilmId = 1
            };
            List<Sessions> sessions = [session];
            Film film = new()
            {
                Id = 1,
                Image = "/images/photo1.png",
                Price = 80,
                Name = "Deadpool 3",
                Description = "Deadpool is back!",
                sessions = sessions
            };
            Assert.Equal(1, film.Id);
            Assert.Equal("/images/photo1.png", film.Image);
            Assert.Equal(80, film.Price);
            Assert.Equal("Deadpool 3", film.Name);
            Assert.Equal("Deadpool is back!", film.Description);
            Assert.Equal(sessions, film.sessions);
        }
    }
}