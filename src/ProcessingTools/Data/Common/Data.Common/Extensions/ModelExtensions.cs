namespace ProcessingTools.Data.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public static class ModelExtensions
    {
        public static PropertyInfo GetIdProperty(this Type modelType)
        {
            var properties = modelType.GetProperties();
            if (properties == null)
            {
                return null;
            }

            Regex matchIdName = new Regex(@"(?i)\A.*id\Z");
            var property = properties.FirstOrDefault(p => matchIdName.IsMatch(p.Name));
            return property;
        }

        public static PropertyInfo GetIdProperty(this Type modelType, Type attributeType)
        {
            var properties = modelType.GetProperties();
            if (properties == null)
            {
                return null;
            }

            var property = properties.FirstOrDefault(p => Attribute.IsDefined(p, attributeType));
            if (property == null)
            {
                return modelType.GetIdProperty();
            }

            return property;
        }

        public static PropertyInfo GetIdProperty<TIdAttribute>(this Type modelType)
            where TIdAttribute : Attribute
        {
            return modelType.GetIdProperty(typeof(TIdAttribute));
        }

        public static object GetIdValue(this object entity)
        {
            var property = entity.GetType().GetIdProperty();
            if (property == null)
            {
                return null;
            }

            return property.GetValue(entity);
        }

        public static object GetIdValue(this object entity, Type attributeType)
        {
            var property = entity.GetType().GetIdProperty(attributeType);
            if (property == null)
            {
                return null;
            }

            return property.GetValue(entity);
        }

        public static object GetIdValue<TIdAttribute>(this object entity)
            where TIdAttribute : Attribute
        {
            return entity.GetIdValue(typeof(TIdAttribute));
        }
    }
}