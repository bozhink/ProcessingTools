namespace ProcessingTools.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using Strings = Resources.ViewModels.Account.Strings;

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = nameof(Strings.Code), ResourceType = typeof(Strings))]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = nameof(Strings.RememberBrowser), ResourceType = typeof(Strings))]
        public bool RememberBrowser { get; set; }

        [Display(Name = nameof(Strings.RememberMe), ResourceType = typeof(Strings))]
        public bool RememberMe { get; set; }
    }
}
