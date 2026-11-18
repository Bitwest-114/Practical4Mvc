using System.ComponentModel.DataAnnotations;

namespace MvcAuthPractice.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Введіть електронну пошту.")]
    [EmailAddress(ErrorMessage = "Введіть правильну електронну пошту.")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Введіть пароль.")]
    public string Password { get; set; } = "";
}
