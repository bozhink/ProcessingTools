﻿namespace ProcessingTools.Documents.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using Constants;

    public class Author : DocumentsAbstractEntity
    {
        private ICollection<Affiliation> affiliations;
        private ICollection<Article> articles;

        public Author()
        {
            this.Id = Guid.NewGuid();
            this.affiliations = new HashSet<Affiliation>();
            this.articles = new HashSet<Article>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfAuthorSurname)]
        public string Surname { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorGivenNames)]
        public string GivenNames { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorPrefix)]
        public string Prefix { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAuthorSuffix)]
        public string Suffix { get; set; }

        public virtual ICollection<Affiliation> Affiliations
        {
            get
            {
                return this.affiliations;
            }

            set
            {
                this.affiliations = value;
            }
        }

        public virtual ICollection<Article> Articles
        {
            get
            {
                return this.articles;
            }

            set
            {
                this.articles = value;
            }
        }
    }
}