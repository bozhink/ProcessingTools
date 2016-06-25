namespace ProcessingTools.Common.Models
{
    using System;

    public interface IModelWithUserInformation
    {
        DateTime DateCreated { get; set; }

        DateTime DateModified { get; set; }

        string CreatedByUser { get; set; }

        string ModifiedByUser { get; set; }
    }
}
