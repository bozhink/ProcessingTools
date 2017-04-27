namespace ProcessingTools.Data.Common.Mongo.Tests.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Book
    {
        public Book()
        {
        }

        public Book(string title, string isbn, Author author)
        {
            this.Title = title;
            this.Isbn = isbn;
            this.Author = author;
        }

        public Author Author { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}) by {2}", this.Title, this.Isbn, this.Author);
        }
    }
}