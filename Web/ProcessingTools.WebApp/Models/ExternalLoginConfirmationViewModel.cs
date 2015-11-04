namespace ProcessingTools.WebApp.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // Models returned by AccountController actions.
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hometown")]
        public string Hometown { get; set; }
    }
}