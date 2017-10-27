namespace ProcessingTools.Journals.Services.Data.Models.ServiceModels
{
    using System;
    using System.Collections.Generic;
    using Contracts.Models;
    using ProcessingTools.Models.Contracts;

    internal class InstitutionDetailsServiceModel : IInstitutionDetails, IServiceModel
    {
        public string AbbreviatedName { get; set; }

        public ICollection<IAddress> Addresses { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public string Id { get; set; }

        public string ModifiedBy { get; set; }

        public string Name { get; set; }
    }
}
