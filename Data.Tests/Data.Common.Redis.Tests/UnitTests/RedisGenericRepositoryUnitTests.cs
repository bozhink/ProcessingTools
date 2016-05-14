namespace ProcessingTools.Data.Common.Redis.Tests.UnitTests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;

    using ProcessingTools.Data.Common.Redis.Contracts;
    using ProcessingTools.Data.Common.Redis.Repositories;

    [TestClass]
    public class RedisGenericRepositoryUnitTests
    {
        private const string FakeContextKey = "FakeContextKey";
        private const int NumberOfItemsToAdd = 1000;

        private IRedisClientProvider provider;
        private Tweet tweet;

        [TestInitialize]
        public void TestInitialize()
        {
            this.provider = new RedisClientProviderMock();
            this.tweet = new Tweet
            {
                Content = Guid.NewGuid().ToString()
            };
        }

        [TestMethod]
        public void RedisGenericRepository_WithValidParametersInConstructor_ShouldReturnValidObject()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            Assert.IsNotNull(repository, "Repository should not be null.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_WithNullProviderInConstructor_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(null);
        }

        [TestMethod]
        public void RedisGenericRepository_WithNullProviderInConstructor_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(null);
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("provider", e.ParamName, "ParamName should have value 'provider'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AddWithNullContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.Add(null, this.tweet).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AddWithNullContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.Add(null, this.tweet).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AddWithEmptyContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.Add(string.Empty, this.tweet).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AddWithEmptyContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.Add(string.Empty, this.tweet).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AddWithWhitespaceContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.Add("     ", this.tweet).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AddWithWhitespaceContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.Add("     ", this.tweet).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AddWithNullEntity_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.Add(FakeContextKey, null).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AddWithNullEntity_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.Add(FakeContextKey, null).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("entity", e.ParamName, "ParamName should have value 'entity'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AddSingleValidEntity_ShouldWork()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);

            this.tweet.Id = 0;
            repository.Add(FakeContextKey, this.tweet).Wait();

            Assert.AreEqual(1, this.tweet.Id, "Id od inserted object should be 1.");

            var items = repository.All(FakeContextKey).Result.ToList();
            Assert.AreEqual(1, items.Count, "Number od items should be 1.");

            var firstItem = items.FirstOrDefault();
            Assert.AreEqual(this.tweet.Id, firstItem.Id, "Id should match.");
            Assert.AreEqual(this.tweet.PostedOn.ToString(), firstItem.PostedOn.ToString(), "PostedOn should match.");
            Assert.AreEqual(this.tweet.Content, firstItem.Content, "Content should match.");
        }

        [TestMethod]
        [Timeout(10000)]
        public void RedisGenericRepository_AddMultipleValidEntities_ShouldWork()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);

            for (int i = 0; i < NumberOfItemsToAdd; ++i)
            {
                var tweet = new Tweet
                {
                    Id = 0,
                    Content = Guid.NewGuid().ToString(),
                    PostedOn = DateTime.UtcNow + new TimeSpan(0, 0, 0, 0, i)
                };

                repository.Add(FakeContextKey, tweet).Wait();

                Assert.AreEqual(i + 1, tweet.Id, $"Id od inserted object should be {i + 1}.");

                var items = repository.All(FakeContextKey).Result.ToList();
                Assert.AreEqual(i + 1, items.Count, "Number od items should be 1.");

                var lastItem = repository.All(FakeContextKey, i, 1).Result.FirstOrDefault();
                Assert.AreEqual(tweet.Id, lastItem.Id, "Id should match.");
                Assert.AreEqual(tweet.PostedOn.ToString(), lastItem.PostedOn.ToString(), "PostedOn should match.");
                Assert.AreEqual(tweet.Content, lastItem.Content, "Content should match.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllWithNullContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(null).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllWithNullContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(null).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllWithEmptyContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(string.Empty).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllWithEmptyContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(string.Empty).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllWithWhitespaceContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All("     ").Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllWithWhitespaceContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All("     ").Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllSkipTakeWithNullContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(null, 0, 1).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllSkipTakeWithNullContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(null, 0, 1).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllSkipTakeWithEmptyContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(string.Empty, 0, 1).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllSkipTakeWithEmptyContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(string.Empty, 0, 1).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RedisGenericRepository_AllSkipTakeWithWhitespaceContext_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All("     ", 0, 1).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllSkipTakeWithWhitespaceContext_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All("     ", 0, 1).Wait();
            }
            catch (ArgumentNullException e)
            {
                Assert.AreEqual("context", e.ParamName, "ParamName should have value 'context'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void RedisGenericRepository_AllSkipTakeWithInvalidSkip_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(FakeContextKey, -1, 1).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllSkipTakeWithInvalidSkip_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(FakeContextKey, -1, 1).Wait();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("skip", e.ParamName, "ParamName should have value 'skip'.");
            }
        }

        [TestMethod]
        [Timeout(1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void RedisGenericRepository_AllSkipTakeWithInvalidTake_ShouldThow()
        {
            var repository = new RedisGenericRepository<Tweet>(this.provider);
            repository.All(FakeContextKey, 0, 0).Wait();
        }

        [TestMethod]
        [Timeout(1000)]
        public void RedisGenericRepository_AllSkipTakeWithInvalidTake_ShouldThowArgumentNullExceptionWithCorrectParameterName()
        {
            try
            {
                var repository = new RedisGenericRepository<Tweet>(this.provider);
                repository.All(FakeContextKey, 0, 0).Wait();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("take", e.ParamName, "ParamName should have value 'take'.");
            }
        }
    }
}
