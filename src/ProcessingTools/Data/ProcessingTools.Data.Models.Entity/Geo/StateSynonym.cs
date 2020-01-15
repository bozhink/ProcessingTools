// <copyright file="StateSynonym.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Entity.Geo
{
    /// <summary>
    /// State synonym.
    /// </summary>
    public class StateSynonym : Synonym
    {
        /// <summary>
        /// Gets or sets the ID of the state.
        /// </summary>
        public virtual int StateId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public virtual State State { get; set; }
    }
}
