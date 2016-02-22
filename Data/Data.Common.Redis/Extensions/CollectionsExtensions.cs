namespace ProcessingTools.Data.Common.Redis.Extensions
{
    using System;
    using System.Linq;
    using ProcessingTools.Data.Common.Models.Contracts;
    using ServiceStack.Redis;

    public static class CollectionsExtensions
    {
        public static int GetMaximalId(this IRedisList list)
        {
            int maxId;

            if (list.Any())
            {
                maxId = list.Max(i => i.Deserialize<IEntity>().Id);
            }
            else
            {
                maxId = 0;
            }

            return maxId;
        }

        public static void AddEntity<T>(this IRedisList list, T entity)
            where T : IEntity
        {
            list.Add(entity.Serialize());
        }

        public static void RemoveEntity<T>(this IRedisList list, T entity)
            where T : IEntity
        {
            list.Remove(entity.Serialize());
        }

        public static void RemoveEntity<T>(this IRedisList list, int id)
            where T : IEntity
        {
            var entityToBeRemoved = list.Select(i => i.Deserialize<T>())
                .FirstOrDefault(i => i.Id == id);

            if (entityToBeRemoved == null)
            {
                throw new ApplicationException("Entity not found.");
            }

            list.Remove(entityToBeRemoved.Serialize());
        }
    }
}
