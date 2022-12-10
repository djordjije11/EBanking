using EBanking.BusinessLayer.Interfaces;
using EBanking.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Web.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(IUserLogic userLogic)
        {
            UserLogic = userLogic;
        }

        public IUserLogic UserLogic { get; }
        [HttpGet]
        public async Task<IActionResult> Index(string? successMessage, string? errorMessage)
        {
            var users = await UserLogic.GetAllUsersAsync();

            ViewBag.SuccessMessage = successMessage;
            ViewBag.ErrorMessage = errorMessage;

            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel userCreate)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            if(ModelState.IsValid)
            {
                try
                {
                    await UserLogic.AddUserAsync(userCreate.FirstName, userCreate.LastName, userCreate.Email, userCreate.Password);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }
            return View(userCreate);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await UserLogic.FindUserAsync(id);
                return View(new UserEditViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel userEdit)
        {
            // OVO POGLEDAJ
            if (!ModelState.IsValid)
            {
                return View(userEdit);
            }
            try
            {
                var user = await UserLogic.UpdateUserAsync(userEdit.Id, userEdit.Email, userEdit.OldPassword, userEdit.NewPassword);
                return RedirectToAction("Index", new { successMessage = $"Uspesno izmenjeni podaci korisnika {userEdit.FirstName} {userEdit.LastName}." });
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(userEdit);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await UserLogic.DeleteUserAsync(id, "");
                return RedirectToAction("Index", new { successMessage = "Uspesno ste obrisali korisnika" });
            }
            catch
            {
                return RedirectToAction("Index", new { errorMessage = "Niste uspeli da obrisete zeljenog korisnika" });
            }
        }
    }
}
