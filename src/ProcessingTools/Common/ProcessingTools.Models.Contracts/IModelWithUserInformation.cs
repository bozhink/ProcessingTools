namespace ProcessingTools.Models.Contracts
{
    using System;

    public interface IModelWithUserInformation
    {
        DateTime DateCreated { get; }

        DateTime DateModified { get; }

        string CreatedByUser { get; }

        string ModifiedByUser { get; }
    }
}
