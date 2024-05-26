using Microsoft.AspNetCore.Mvc;

namespace ProjectCinema.Interfaces
{
    public interface IHome
    {
        IActionResult Index();
        Task<IActionResult> Help();
        Task<IActionResult> Movie(int id);
        Task<IActionResult> BuyTickets(int Id);
        IActionResult ErrorTickets();
    }
}
