using Microsoft.AspNetCore.Mvc;

namespace ProjectCinema.Interfaces
{
    public interface IAccount
    {
        Task<IActionResult> Login();
        Task<IActionResult> Register();
        Task<IActionResult> Logout();
        Task<IActionResult> Profile();
        Task<IActionResult> ChangePassword();
        Task<IActionResult> History();
    }
}
