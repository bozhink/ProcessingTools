namespace ProcessingTools.Documents.Services.Data.DataModels.Publishers
{
    using System;
    using ProcessingTools.Models.Abstractions;
    using ProcessingTools.Models.Contracts.Documents;

    public class AddressEntity : ModelWithUserInformation, IAddress
    {
        public AddressEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
