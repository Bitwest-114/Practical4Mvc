namespace MvcAuthPractice.Models;

// Модель зберігає ім'я, пошту та пароль користувача.
public class User
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
