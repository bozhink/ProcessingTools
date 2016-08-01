namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class AddressViewModel
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        [Display(Name = "Address")]
        public string AddressString { get; set; }
    }
}
