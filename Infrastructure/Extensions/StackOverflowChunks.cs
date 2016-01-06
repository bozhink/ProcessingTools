namespace ProcessingTools.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class StackOverflowChunks
    {
        /*
         * http://stackoverflow.com/questions/325426/programmatic-equivalent-of-defaulttype
         */

        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        public static T GetDefaultValue<T>()
        {
            // We want an Func<T> which returns the default.
            // Create that expression here.
            Expression<Func<T>> e = Expression.Lambda<Func<T>>(
                // The default value, always get what the *code* tells us.
                Expression.Default(typeof(T))
            );

            // Compile and return the value.
            return e.Compile()();
        }

        public static object GetDefaultValue(this Type type)
        {
            // Validate parameters.
            if (type == null) throw new ArgumentNullException("type");

            // We want an Func<object> which returns the default.
            // Create that expression here.
            Expression<Func<object>> e = Expression.Lambda<Func<object>>(
                // Have to convert to object.
                Expression.Convert(
                    // The default value, always get what the *code* tells us.
                    Expression.Default(type), typeof(object)
                )
            );

            // Compile and return the value.
            return e.Compile()();
        }

        //a thread-safe way to hold default instances created at run-time
        private static ConcurrentDictionary<Type, object> typeDefaults = new ConcurrentDictionary<Type, object>();

        public static object GetDefaultValueConcurrentDictionary(this Type type)
        {
            return type.IsValueType ? typeDefaults.GetOrAdd(type, t => Activator.CreateInstance(t)) : null;
        }

        private static T GetDefault<T>()
        {
            return default(T);
        }

        public static object GetDefaultObject(Type t)
        {
            Func<object> f = GetDefault<object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(t).Invoke(null, null);
        }

        private static Dictionary<Type, Delegate> lambdasMap = new Dictionary<Type, Delegate>();

        private static object GetNull(Type type)
        {
            Delegate func;
            if (!lambdasMap.TryGetValue(type, out func))
            {
                var body = Expression.Default(type);
                var lambda = Expression.Lambda(body);
                func = lambda.Compile();
                lambdasMap[type] = func;
            }
            return func.DynamicInvoke();
        }

        public static object MyDefault(this Type type)
        {
            object output = null;

            if (type.IsValueType)
            {
                output = Activator.CreateInstance(type);
            }

            return output;
        }

        public static object GetDefaultValue1(this Type t)
        {
            if (!t.IsValueType || Nullable.GetUnderlyingType(t) != null)
            {
                return null;
            }

            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// returns the default value of a specified type
        /// </summary>
        /// <param name="type"></param>
        public static object GetDefault1(this Type type)
        {
            return type.IsValueType ? (!type.IsGenericType ? Activator.CreateInstance(type) : type.GenericTypeArguments[0].GetDefault1()) : null;
        }
    }
}