namespace ProjectCinema.Models
{
    public class Ticket
    {
        public int Id { get; set; }        
        public string FilmName { get; set; }
        public double Price { get; set; }
        public string? SenderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Time { get; set; }
    }
}
