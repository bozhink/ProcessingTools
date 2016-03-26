namespace ProcessingTools.Data.Common.Models.Contracts
{
    using System;

    public interface IEntityWithUserInformation
    {
        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

        string CreatedByUserId { get; set; }

        string ModifiedByUserId { get; set; }
    }
}