using System;
using System.ComponentModel.DataAnnotations;

namespace Pay.Billeads.ViewModel
{
  public class RegisterViewModel
  {
    [Required]
    [StringLength(60, MinimumLength = 2)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string Password { get; set; }
  }
}

