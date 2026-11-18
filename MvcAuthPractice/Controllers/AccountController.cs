using Microsoft.AspNetCore.Mvc;
using MvcAuthPractice.Models;

namespace MvcAuthPractice.Controllers;

public class AccountController : Controller
{
    // Для цієї навчальної роботи акаунти зберігаються в пам'яті.
    private static List<User> users = new List<User>();

    public IActionResult Register()
    {
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(User user)
    {
        if (string.IsNullOrEmpty(user.Name) ||
            string.IsNullOrEmpty(user.Email) ||
            string.IsNullOrEmpty(user.Password))
        {
            ViewBag.Error = "Заповни всі поля.";
            return View(user);
        }

        if (user.Password.Length < 4)
        {
            ViewBag.Error = "Пароль має містити хоча б 4 символи.";
            return View(user);
        }

        foreach (User savedUser in users)
        {
            if (savedUser.Email == user.Email)
            {
                ViewBag.Error = "Такий акаунт уже зареєстрований.";
                return View(user);
            }
        }

        users.Add(user);
        HttpContext.Session.SetString("UserName", user.Name);
        return RedirectToAction("Profile");
    }

    public IActionResult Login()
    {
        return View(new User());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(User user)
    {
        foreach (User savedUser in users)
        {
            if (savedUser.Email == user.Email && savedUser.Password == user.Password)
            {
                HttpContext.Session.SetString("UserName", savedUser.Name);
                return RedirectToAction("Profile");
            }
        }

        ViewBag.Error = "Неправильна пошта або пароль.";
        return View(user);
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
