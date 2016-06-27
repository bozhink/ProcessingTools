namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class AddressViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        public string AddressString { get; set; }
    }
}
