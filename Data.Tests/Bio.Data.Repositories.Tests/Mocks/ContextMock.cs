namespace ProcessingTools.Bio.Data.Repositories.Tests.Mocks
{
    using Models;
    using Moq;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ContextMock
    {
        private HashSet<Tweet> items;

        public ContextMock()
        {
            this.items = new HashSet<Tweet>();
        }

        public Mock<BioDbContext> MockObject
        {
            get
            {
                var dbsetMock = new Mock<DbSet<Tweet>>();
                dbsetMock
                    .Setup(s => s.Add(It.IsAny<Tweet>()))
                    .Returns((Tweet t) =>
                    {
                        this.items.Add(t);
                        System.Console.WriteLine(t);
                        return t;
                    });

                dbsetMock
                    .Setup(s => s.Attach(It.IsAny<Tweet>()))
                    .Returns((Tweet t) => t);

                dbsetMock
                    .Setup(s => s.Remove(It.IsAny<Tweet>()))
                    .Returns((Tweet t) =>
                    {
                        this.items.Remove(t);
                        return t;
                    });

                dbsetMock
                    .Setup(s => s.Find(It.IsAny<object>()))
                    .Returns((object id) =>
                    {
                        return this.items.FirstOrDefault(t => t.Id == (int)id);
                    });

                // Mock for DbSet.AsQueriable
                dbsetMock.As<IQueryable<Tweet>>()
                    .Setup(m => m.Provider)
                    .Returns(this.items.AsQueryable().Provider);
                dbsetMock.As<IQueryable<Tweet>>()
                    .Setup(m => m.Expression)
                    .Returns(this.items.AsQueryable().Expression);
                dbsetMock.As<IQueryable<Tweet>>()
                    .Setup(m => m.ElementType)
                    .Returns(this.items.AsQueryable().ElementType);
                dbsetMock.As<IQueryable<Tweet>>()
                    .Setup(m => m.GetEnumerator())
                    .Returns(this.items.AsQueryable().GetEnumerator());

                var contextMock = new Mock<BioDbContext>("Fake connection string");
                contextMock
                    .Setup(context => context.Set<Tweet>())
                    .Returns(dbsetMock.Object);

                return contextMock;
            }
        }
    }
}
