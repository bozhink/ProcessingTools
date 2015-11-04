﻿namespace ProcessingTools.WebApp.Models
{
    using System.Collections.Generic;

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}