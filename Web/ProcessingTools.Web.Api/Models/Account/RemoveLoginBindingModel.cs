﻿namespace ProcessingTools.Web.Api.Models.Account
{
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "Login provider")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "Provider key")]
        public string ProviderKey { get; set; }
    }
}