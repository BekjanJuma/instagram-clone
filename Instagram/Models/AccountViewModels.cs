using Instagram.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Instagram.Models
{
   public class ExternalLoginConfirmationViewModel
   {
      [Required]
      [Display(Name = "Email")]
      public string Email { get; set; }
   }

   public class ExternalLoginListViewModel
   {
      public string ReturnUrl { get; set; }
   }

   public class SendCodeViewModel
   {
      public string SelectedProvider { get; set; }
      public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
      public string ReturnUrl { get; set; }
      public bool RememberMe { get; set; }
   }

   public class VerifyCodeViewModel
   {
      [Required]
      public string Provider { get; set; }

      [Required]
      [Display(Name = "Code")]
      public string Code { get; set; }
      public string ReturnUrl { get; set; }

      [Display(Name = "Remember this browser?")]
      public bool RememberBrowser { get; set; }

      public bool RememberMe { get; set; }
   }

   public class ForgotViewModel
   {
      [Required]
      [Display(Name = @"Email", ResourceType = typeof(Strings))]
      public string Email { get; set; }
   }

   public class LoginViewModel
   {
      [Required]
      [Display(Name = @"Username", ResourceType = typeof(Strings))]
      public string UserName { get; set; }

      [Required]
      [DataType(DataType.Password)]
      [Display(Name = @"Password", ResourceType = typeof(Strings))]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
   }

   public class RegisterViewModel
   {
      [Required]
      [Display(Name = @"Username", ResourceType = typeof(Strings))]
      public string UserName { get; set; }

      [Required]
      [EmailAddress]
      [Display(Name = @"Email", ResourceType = typeof(Strings))]
      public string Email { get; set; }

      [Required]
      [Display(Name = @"Avatar", ResourceType = typeof(Strings))]
      public HttpPostedFileBase Avatar { get; set; }

      [Display(Name = @"Fullname", ResourceType = typeof(Strings))]
      public string FullName { get; set; }

      [Display(Name = @"Aboutme", ResourceType = typeof(Strings))]
      public string AboutMe { get; set; }

      [Display(Name = @"Phone_number", ResourceType = typeof(Strings))]
      public string PhoneNumber { get; set; }

      [Display(Name = @"Sex", ResourceType = typeof(Strings))]
      public bool Sex { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = @"Password", ResourceType = typeof(Strings))]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = @"Confirm_password", ResourceType = typeof(Strings))]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }
   }

   public class ResetPasswordViewModel
   {
      [Required]
      [EmailAddress]
      [Display(Name = @"Email", ResourceType = typeof(Strings))]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = @"Password", ResourceType = typeof(Strings))]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = @"Confirm_password", ResourceType = typeof(Strings))]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      public string Code { get; set; }
   }

   public class ForgotPasswordViewModel
   {
      [Required]
      [EmailAddress]
      [Display(Name = @"Email", ResourceType = typeof(Strings))]
      public string Email { get; set; }
   }
}
