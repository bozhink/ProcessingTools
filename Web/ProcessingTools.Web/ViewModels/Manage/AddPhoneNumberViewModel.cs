namespace ProcessingTools.Web.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;
    using Strings = Resources.ViewModels.Manage.Strings;

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = nameof(Strings.PhoneNumber), ResourceType = typeof(Strings))]
        public string Number { get; set; }
    }
}
