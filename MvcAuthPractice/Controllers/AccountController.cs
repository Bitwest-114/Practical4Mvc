using Microsoft.AspNetCore.Mvc;
using MvcAuthPractice.Models;

namespace MvcAuthPractice.Controllers;

public class AccountController : Controller
{
    // Для цієї навчальної роботи акаунти зберігаються в пам'яті.
    private static readonly List<User> users = new List<User>();

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        bool emailExists = users.Any(savedUser =>
            savedUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

        if (emailExists)
        {
            ViewBag.Error = "Такий акаунт уже зареєстрований.";
            return View(user);
        }

        users.Add(user);
        HttpContext.Session.SetString("UserName", user.Name);
        return RedirectToAction("Profile");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        User? user = users.FirstOrDefault(savedUser =>
            savedUser.Email.Equals(model.Email, StringComparison.OrdinalIgnoreCase) &&
            savedUser.Password == model.Password);

        if (user == null)
        {
            ViewBag.Error = "Неправильна пошта або пароль.";
            return View(model);
        }

        HttpContext.Session.SetString("UserName", user.Name);
        return RedirectToAction("Profile");
    }

    public IActionResult Profile()
    {
        string? userName = HttpContext.Session.GetString("UserName");

        if (userName == null)
        {
            return RedirectToAction("Login");
        }

        return View("Profile", userName);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
