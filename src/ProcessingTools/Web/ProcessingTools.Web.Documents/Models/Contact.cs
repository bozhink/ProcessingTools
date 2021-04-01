// <copyright file="Contact.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Models
{
    /// <summary>
    /// Contact Model.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the ID of the contact.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the contact.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the contact.
        /// </summary>
        public string LastName { get; set; }
    }
}
