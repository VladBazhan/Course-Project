using Microsoft.AspNetCore.Mvc;

namespace ProjectCinema.Interfaces
{
    public interface IAdmin
    {
        Task<IActionResult> ManageAccounts();
        Task<IActionResult> ManageFilms();
        Task<IActionResult> ManageArticles();
        Task<IActionResult> AddAccount();
        Task<IActionResult> AddFilm();
        Task<IActionResult> AddArticle();
        Task<IActionResult> EditAccount(string id);
        Task<IActionResult> EditFilm(int id);
        Task<IActionResult> EditArticle(int id);
        Task<IActionResult> DeleteAccount(string id);
        Task<IActionResult> DeleteFilm(int id);
        Task<IActionResult> DeleteArticle(int id);
    }
}
