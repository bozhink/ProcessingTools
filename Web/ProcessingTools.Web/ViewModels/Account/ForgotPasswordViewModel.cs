namespace ProcessingTools.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Strings = Resources.ViewModels.Account.Strings;

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; }
    }
}
