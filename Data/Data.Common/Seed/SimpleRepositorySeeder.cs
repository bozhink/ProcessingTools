namespace ProcessingTools.Data.Common.Seed
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Repositories.Contracts;

    public class SimpleRepositorySeeder<TEntity>
        where TEntity : class
    {
        private IGenericRepository<TEntity> repository;

        public SimpleRepositorySeeder(IGenericRepository<TEntity> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }

        public async Task Seed<TSeedModel>(params TSeedModel[] data)
            where TSeedModel : class
        {
            if (data == null || data.Length < 1)
            {
                throw new ArgumentNullException("data");
            }

            foreach (var item in data)
            {
                var entity = Map<TSeedModel, TEntity>(item);
                await this.repository.Add(entity);
            }

            await this.repository.SaveChanges();
        }

        private Tout Map<Tin, Tout>(Tin item)
            where Tin : class
            where Tout : class
        {
            Tout result = Activator.CreateInstance<Tout>();

            var propertiesIn = typeof(Tin).GetProperties();
            var propertiesOut = typeof(Tout).GetProperties();
            foreach (var propertyIn in propertiesIn)
            {
                var propertyOut = propertiesOut
                    .FirstOrDefault(p => p.Name == propertyIn.Name && p.PropertyType == propertyIn.PropertyType);

                propertyOut?.SetValue(result, propertyIn.GetValue(item));
            }

            return result;
        }
    }
}