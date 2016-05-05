namespace ProcessingTools.Data.Common.Elasticsearch.Tests.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Moq;
    using Nest;
    using Repositories;

    [TestClass]
    public class ElasticsearchGenericRepositoryUnitTests
    {
        private const string IdShouldBeUnchangedMessage = "Id should be unchanged";
        private const string UserShouldBeUnchangedMessage = "User should be unchanged";
        private const string PostDateShouldBeUnchangedMessage = "PostDate should be unchanged";
        private const string MessageShouldBeUnchangedMessage = "Message should be unchanged";

        private static readonly Random Random = new Random((int)DateTime.Now.ToBinary());

        private IElasticContextProvider contextProvider;
        private IElasticClientProvider clientProvider;

        private Tweet tweet;

        [TestInitialize]
        public void Initialize()
        {
            var contextProviderMock = new Mock<IElasticContextProvider>();
            contextProviderMock.Setup(p => p.Create()).Returns(new IndexName
                {
                    Name = Guid.NewGuid().ToString()
                });

            this.contextProvider = contextProviderMock.Object;

            var clientMock = new Mock<IElasticClient>();
            clientMock.Setup(c => c.IndexExists(It.IsAny<Indices>(), null)).Returns(new ExistsResponse());
            clientMock.Setup(c => c.IndexExistsAsync(It.IsAny<Indices>(), null)).Returns(new Task<IExistsResponse>(() => new ExistsResponse()));

            var clientProviderMock = new Mock<IElasticClientProvider>();
            clientProviderMock.Setup(p => p.Create()).Returns(clientMock.Object);

            this.clientProvider = clientProviderMock.Object;

            this.tweet = new Tweet
            {
                Id = Random.Next(),
                User = "kimchy",
                PostDate = DateTime.Now,
                Message = Guid.NewGuid().ToString()
            };
        }

        [TestMethod]
        public void ElasticsearchGenericRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new ElasticsearchGenericRepository<Tweet>(this.contextProvider, this.clientProvider);

            Assert.IsNotNull(repository, "Repository should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElasticsearchGenericRepository_WithNullContextProviderInConstructor_ShouldTrhow()
        {
            var repository = new ElasticsearchGenericRepository<Tweet>(null, this.clientProvider);
        }

        [TestMethod]
        public void ElasticsearchGenericRepository_WithNullContextProviderInConstructor_ShouldTrhowWithCorrectMessage()
        {
            try
            {
                var repository = new ElasticsearchGenericRepository<Tweet>(null, this.clientProvider);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("contextProvider", e.ParamName, "ParamName should be correct.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElasticsearchGenericRepository_WithNullClientProviderInConstructor_ShouldTrhow()
        {
            var repository = new ElasticsearchGenericRepository<Tweet>(this.contextProvider, null);
        }

        [TestMethod]
        public void ElasticsearchGenericRepository_WithNullClientProviderInConstructor_ShouldTrhowWithCorrectMessage()
        {
            try
            {
                var repository = new ElasticsearchGenericRepository<Tweet>(this.contextProvider, null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("clientProvider", e.ParamName, "ParamName should be correct.");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElasticsearchGenericRepository_WithNullProvidersInConstructor_ShouldTrhow()
        {
            var repository = new ElasticsearchGenericRepository<Tweet>(null, null);
        }

        [TestMethod]
        [Ignore]
        [Timeout(5000)]
        public void ElasticsearchGenericRepository_AddValidEntity_ShouldWork()
        {
            var repository = new ElasticsearchGenericRepository<Tweet>(this.contextProvider, this.clientProvider);

            var clonedTweet = new Tweet
            {
                Id = this.tweet.Id,
                Message = this.tweet.Message,
                PostDate = this.tweet.PostDate,
                User = this.tweet.User
            };

            Assert.IsFalse(this.clientProvider.Create().IndexExists(this.contextProvider.Create().Name).Exists, "Index should not exist.");

            repository.Add(this.tweet).Wait();

            Assert.AreEqual(clonedTweet.Id, this.tweet.Id, IdShouldBeUnchangedMessage);
            Assert.AreEqual(clonedTweet.Message, this.tweet.Message, MessageShouldBeUnchangedMessage);
            Assert.AreEqual(clonedTweet.PostDate, this.tweet.PostDate, PostDateShouldBeUnchangedMessage);
            Assert.AreEqual(clonedTweet.User, this.tweet.User, UserShouldBeUnchangedMessage);
        }
    }
}
