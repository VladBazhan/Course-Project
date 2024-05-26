namespace ProjectCinema.Models
{
    public class ModelBuyTickets
    {
        public string Data { get; set; }
        public Film Film { get; set; } = new Film();
    }
}
