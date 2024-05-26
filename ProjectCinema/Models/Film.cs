using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCinema.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string? Image {  get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Sessions> sessions { get; set; } = new List<Sessions>();
    }
}
