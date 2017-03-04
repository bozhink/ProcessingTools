namespace ProcessingTools.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Constants;
    using Strings = Resources.ViewModels.Account.Strings;

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.PasswordMaximalLength, ErrorMessageResourceName = nameof(Strings.PasswordErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.PasswordMinimalLength)]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.ConfirmPassword), ResourceType = typeof(Strings))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(Strings.ConfirmPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(Strings.Code), ResourceType = typeof(Strings))]
        public string Code { get; set; }
    }
}
