namespace ProcessingTools.Data.Common.Mongo.Factories
{
    using System;
    using System.Linq;
    using ProcessingTools.Common.Attributes;

    public static class CollectionNameFactory
    {
        public static string Create(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (entityType.GetCustomAttributes(typeof(CollectionNameAttribute), false)?.SingleOrDefault() is CollectionNameAttribute collectioNameAttribute)
            {
                return collectioNameAttribute.Name;
            }
            else
            {
                string name = entityType.Name.ToLowerInvariant();
                int nameLength = name.Length;
                if (name[nameLength - 1] == 'y')
                {
                    return $"{name.Substring(0, nameLength - 1)}ies";
                }
                else
                {
                    return $"{name}s";
                }
            }
        }

        public static string Create<TEntity>()
        {
            return Create(typeof(TEntity));
        }
    }
}
