// <copyright file="Book.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Integration.Tests.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Book.
    /// </summary>
    internal class Book
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="title">Title of the book.</param>
        /// <param name="isbn">ISBN of the book.</param>
        /// <param name="author">Author of the book.</param>
        public Book(string title, string isbn, Author author)
        {
            this.Title = title;
            this.Isbn = isbn;
            this.Author = author;
        }

        /// <summary>
        /// Gets or sets the ID of the book.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ISBN of the book.
        /// </summary>
        public string Isbn { get; set; }

        /// <summary>
        /// Gets or sets the author of the book.
        /// </summary>
        public Author Author { get; set; }

        /// <inheritdoc/>
        public override string ToString() => $"{this.Title} ({this.Isbn}) by {this.Author}";
    }
}
