namespace ProcessingTools.Tests.Library
{
    using System;
    using System.Reflection;

    public static class PrivateField
    {
        public static object GetInstanceField<T>(object instance, string fieldName)
        {
            return GetInstanceField(typeof(T), instance, fieldName);
        }

        public static object GetInstanceField(Type type, object instance, string fieldName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName));
            }

            var bindFlags = BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static;

            var fieldInfo = type.GetField(fieldName, bindFlags);
            return fieldInfo?.GetValue(instance);
        }
    }
}
