namespace ProcessingTools.Documents.Services.Data.Models.Contracts
{
    using System;

    public interface ICountryServiceModel
    {
        int Id { get; set; }

        string Name { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

        string CreatedByUserId { get; set; }

        string ModifiedByUserId { get; set; }
    }
}