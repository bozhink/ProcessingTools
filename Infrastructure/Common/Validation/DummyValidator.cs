namespace ProcessingTools.Common.Validation
{
    using System;
    using System.Linq.Expressions;

    public static class DummyValidator
    {
        /// <summary>
        /// Checks database entity dummy parameter with name 'entity' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the entity. It should be reference object.</typeparam>
        /// <param name="entity">Dummy parameter with name 'entity' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateEntity<T>(T entity)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity));
            }
        }

        /// <summary>
        /// Checks database entity dummy parameter with name 'entity' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the entity. It should be reference object.</typeparam>
        /// <param name="entity">Dummy parameter with name 'entity' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateEntity<T>(T entity, string message)
            where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(paramName: nameof(entity), message: message);
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'id' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <param name="id">Dummy parameter with name 'id' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateId(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(paramName: nameof(id));
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'id' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <param name="id">Dummy parameter with name 'id' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateId(object id, string message)
        {
            if (id == null)
            {
                throw new ArgumentNullException(paramName: nameof(id), message: message);
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'filter' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for filter expression.</typeparam>
        /// <param name="filter">Dummy parameter with name 'filter' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateFilter<T>(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(paramName: nameof(filter));
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'filter' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for filter expression.</typeparam>
        /// <param name="filter">Dummy parameter with name 'filter' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateFilter<T>(Expression<Func<T, bool>> filter, string message)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(paramName: nameof(filter), message: message);
            }
        }
    }
}
