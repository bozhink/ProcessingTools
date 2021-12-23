﻿// <copyright file="EnvoTermResponseModel.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Bio.Environments
{
    /// <summary>
    /// ENVO Term response model.
    /// </summary>
    public class EnvoTermResponseModel
    {
        private string envoId;

        /// <summary>
        /// Gets or sets content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets entity ID.
        /// </summary>
        public string EntityId { get; set; }

        /// <summary>
        /// Gets or sets ENVO ID.
        /// </summary>
        public string EnvoId
        {
            get
            {
                return this.envoId;
            }

            set
            {
                this.envoId = "ENVO_" + value?.Substring(5);
            }
        }
    }
}
