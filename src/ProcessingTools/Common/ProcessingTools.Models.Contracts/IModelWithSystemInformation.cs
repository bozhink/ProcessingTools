namespace ProcessingTools.Models.Contracts
{
    using System;

    public interface IModelWithSystemInformation
    {
        string CreatedBy { get; }

        DateTime CreatedOn { get; }

        string ModifiedBy { get; }

        DateTime ModifiedOn { get; }

        byte[] Timestamp { get; }
    }
}
