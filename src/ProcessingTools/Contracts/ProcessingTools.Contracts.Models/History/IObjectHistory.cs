// <copyright file="IObjectHistory.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.History
{
    /// <summary>
    /// Object history model.
    /// </summary>
    public interface IObjectHistory : IStringIdentified, ICreated
    {
        /// <summary>
        /// Gets serialized object data.
        /// </summary>
        string Data { get; }

        /// <summary>
        /// Gets object ID.
        /// </summary>
        string ObjectId { get; }

        /// <summary>
        /// Gets object type.
        /// </summary>
        string ObjectType { get; }

        /// <summary>
        /// Gets assembly name.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Gets assembly version.
        /// </summary>
        string AssemblyVersion { get; }
    }
}
