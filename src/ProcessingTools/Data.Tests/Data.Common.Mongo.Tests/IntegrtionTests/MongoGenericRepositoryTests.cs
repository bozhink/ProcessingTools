namespace ProcessingTools.Data.Common.Mongo.Tests.IntegrtionTests
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ProcessingTools.Data.Common.Mongo.Tests.Fakes;
    using ProcessingTools.Data.Common.Mongo.Tests.Models;
    using ProcessingTools.Data.Mongo.Abstractions;

    [TestClass]
    public class MongoGenericRepositoryTests
    {
        private const string DatabaseName = "MongoGenericRepository_Integration_Tests_F92F80C9";

        /// <summary>
        /// Gets or sets the <see cref="TestContext"/>.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        [Timeout(5000)]
        [Ignore] // Integration test
        public void MongoGenericRepository_AddAllDelete_ShouldWork()
        {
            string databaseName = DatabaseName;
            var provider = new RealMongoDatabaseProvider(databaseName);
            var repository = new MongoGenericRepository<Book>(provider);

            var author = new Author("Patrick", "Rothfuss");
            var book = new Book("How Old Holly Came To Be", "978-0-9847136-3-9", author);
            this.TestContext.WriteLine(book.ToString());

            repository.AddAsync(book).Wait();

            var books = repository.Query.ToList();
            Assert.IsNotNull(books, "Books should not be null.");
            Assert.AreEqual(1, books.Count, "Number of books in db should be 1.");

            var bookFromDb = books.FirstOrDefault();
            Assert.IsNotNull(bookFromDb, "First book in db should not be null.");
            this.TestContext.WriteLine(bookFromDb.ToString());
            this.TestContext.WriteLine(bookFromDb.Id);

            Assert.AreEqual(book.Title, bookFromDb.Title, "Title should match.");
            Assert.AreEqual(book.Isbn, bookFromDb.Isbn, "ISBN should match.");
            Assert.AreEqual(book.Author.FirstName, bookFromDb.Author.FirstName, "Author.FirstName should match.");
            Assert.AreEqual(book.Author.LastName, bookFromDb.Author.LastName, "Author.LastName should match.");

            repository.DeleteAsync(bookFromDb).Wait();

            var booksAfterDeletion = repository.Query?.ToList();
            Assert.IsFalse(booksAfterDeletion?.Count > 0, "Number of books after deletion should be 0.");
        }

        [TestMethod]
        [Timeout(5000)]
        [Ignore] // Integration test
        public void MongoGenericRepository_AddAllFindDelete_ShouldWork()
        {
            string databaseName = DatabaseName;
            var provider = new RealMongoDatabaseProvider(databaseName);
            var repository = new MongoGenericRepository<Book>(provider);

            var author = new Author("Patrick", "Rothfuss");
            var book = new Book("How Old Holly Came To Be", "978-0-9847136-3-9", author);
            this.TestContext.WriteLine(book.ToString());

            repository.AddAsync(book).Wait();

            var books = repository.FindAsync(b => true).Result.ToList();
            Assert.IsNotNull(books, "Books should not be null.");
            Assert.AreEqual(1, books.Count, "Number of books in db should be 1.");

            var bookFromDb = books.FirstOrDefault();
            Assert.IsNotNull(bookFromDb, "First book in db should not be null.");
            this.TestContext.WriteLine(bookFromDb.ToString());
            this.TestContext.WriteLine(bookFromDb.Id);

            Assert.AreEqual(book.Title, bookFromDb.Title, "Title should match.");
            Assert.AreEqual(book.Isbn, bookFromDb.Isbn, "ISBN should match.");
            Assert.AreEqual(book.Author.FirstName, bookFromDb.Author.FirstName, "Author.FirstName should match.");
            Assert.AreEqual(book.Author.LastName, bookFromDb.Author.LastName, "Author.LastName should match.");

            repository.DeleteAsync(bookFromDb).Wait();

            var booksAfterDeletion = repository.Query?.ToList();
            Assert.IsFalse(booksAfterDeletion?.Count > 0, "Number of books after deletion should be 0.");
        }

        [TestMethod]
        [Timeout(5000)]
        [Ignore] // Integration test
        public void MongoGenericRepository_AddAllUpdateDelete_ShouldWork()
        {
            string databaseName = DatabaseName;
            var provider = new RealMongoDatabaseProvider(databaseName);
            var repository = new MongoGenericRepository<Book>(provider);

            var author = new Author("Patrick", "Rothfuss");
            var book = new Book("How Old Holly Came To Be", "978-0-9847136-3-9", author);
            this.TestContext.WriteLine(book.ToString());

            repository.AddAsync(book).Wait();

            var books = repository.Query.ToList();
            Assert.IsNotNull(books, "Books should not be null.");
            Assert.AreEqual(1, books.Count, "Number of books in db should be 1.");

            var bookFromDb = books.FirstOrDefault();
            Assert.IsNotNull(bookFromDb, "First book in db should not be null.");
            this.TestContext.WriteLine(bookFromDb.ToString());
            this.TestContext.WriteLine(bookFromDb.Id);

            Assert.AreEqual(book.Title, bookFromDb.Title, "Title should match.");
            Assert.AreEqual(book.Isbn, bookFromDb.Isbn, "ISBN should match.");
            Assert.AreEqual(book.Author.FirstName, bookFromDb.Author.FirstName, "Author.FirstName should match.");
            Assert.AreEqual(book.Author.LastName, bookFromDb.Author.LastName, "Author.LastName should match.");

            /* Update */
            bookFromDb.Author.FirstName = bookFromDb.Author.FirstName + "1";
            repository.UpdateAsync(bookFromDb).Wait();

            books = repository.Query.ToList();
            Assert.IsNotNull(books, "Books should not be null.");
            Assert.AreEqual(1, books.Count, "Number of books in db should be 1.");

            bookFromDb = books.FirstOrDefault();
            Assert.IsNotNull(bookFromDb, "First book in db should not be null.");
            this.TestContext.WriteLine(bookFromDb.ToString());
            this.TestContext.WriteLine(bookFromDb.Id);

            Assert.AreEqual(book.Title, bookFromDb.Title, "Title should match.");
            Assert.AreEqual(book.Isbn, bookFromDb.Isbn, "ISBN should match.");
            Assert.AreEqual(book.Author.FirstName + "1", bookFromDb.Author.FirstName, "Author.FirstName should match.");
            Assert.AreEqual(book.Author.LastName, bookFromDb.Author.LastName, "Author.LastName should match.");

            repository.DeleteAsync(bookFromDb).Wait();

            var booksAfterDeletion = repository.Query?.ToList();
            Assert.IsFalse(booksAfterDeletion?.Count > 0, "Number of books after deletion should be 0.");
        }
    }
}
