namespace ProcessingTools.Web.Documents.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
