using System.ComponentModel.DataAnnotations;

namespace MvcAuthPractice.Models;

// Навчальна модель користувача.
public class User
{
    [Required(ErrorMessage = "Введіть ім'я.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Введіть електронну пошту.")]
    [EmailAddress(ErrorMessage = "Введіть правильну електронну пошту.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Введіть пароль.")]
    [MinLength(4, ErrorMessage = "Пароль має містити щонайменше 4 символи.")]
    public string Password { get; set; } = "";
}
