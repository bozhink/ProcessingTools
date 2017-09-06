namespace ProcessingTools.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Web;
    using Strings = Resources.ViewModels.Manage.Strings;

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.OldPassword), ResourceType = typeof(Strings))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(ValidationConstants.PasswordMaximalLength, ErrorMessageResourceName = nameof(Strings.NewPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.PasswordMinimalLength)]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.NewPassword), ResourceType = typeof(Strings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Strings.ConfirmPassword), ResourceType = typeof(Strings))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(Strings.ConfirmPasswordErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string ConfirmPassword { get; set; }
    }
}
