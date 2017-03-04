namespace ProcessingTools.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;
    using Strings = Resources.ViewModels.Manage.Strings;

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = nameof(Strings.Code), ResourceType = typeof(Strings))]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = nameof(Strings.PhoneNumber), ResourceType = typeof(Strings))]
        public string PhoneNumber { get; set; }
    }
}
