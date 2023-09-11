using System.ComponentModel;
using Microsoft.Build.Framework;


public class LoginViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required, DisplayName("Remember me?")]
    public bool RememberMe { get; set; }
}
