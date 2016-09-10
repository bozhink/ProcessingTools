namespace ProcessingTools.Common.Validation
{
    using System;
    using System.Linq.Expressions;
    using Constants;
    using Exceptions;

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
        /// Checks service model dummy parameter with name 'model' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the model. It should be reference object.</typeparam>
        /// <param name="model">Dummy parameter with name 'model' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateModel<T>(T model)
            where T : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(paramName: nameof(model));
            }
        }

        /// <summary>
        /// Checks service model dummy parameter with name 'model' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the model. It should be reference object.</typeparam>
        /// <param name="model">Dummy parameter with name 'model' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateModel<T>(T model, string message)
            where T : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(paramName: nameof(model), message: message);
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'update' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the update. It should be reference object.</typeparam>
        /// <param name="update">Dummy parameter with name 'update' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateUpdate<T>(T update)
            where T : class
        {
            if (update == null)
            {
                throw new ArgumentNullException(paramName: nameof(update));
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'update' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the update. It should be reference object.</typeparam>
        /// <param name="update">Dummy parameter with name 'update' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateUpdate<T>(T update, string message)
            where T : class
        {
            if (update == null)
            {
                throw new ArgumentNullException(paramName: nameof(update), message: message);
            }
        }

        /// <summary>
        /// Checks set dummy parameter with name 'set' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the set. It should be reference object.</typeparam>
        /// <param name="set">Dummy parameter with name 'set' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateSet<T>(T set)
            where T : class
        {
            if (set == null)
            {
                throw new ArgumentNullException(paramName: nameof(set));
            }
        }

        /// <summary>
        /// Checks set dummy parameter with name 'set' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the set. It should be reference object.</typeparam>
        /// <param name="set">Dummy parameter with name 'set' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateSet<T>(T set, string message)
            where T : class
        {
            if (set == null)
            {
                throw new ArgumentNullException(paramName: nameof(set), message: message);
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

        /// <summary>
        /// Checks dummy parameter with name 'sort' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for sort expression.</typeparam>
        /// <param name="sort">Dummy parameter with name 'sort' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateSort<T>(Expression<Func<T, object>> sort)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(paramName: nameof(sort));
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'sort' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for sort expression.</typeparam>
        /// <param name="sort">Dummy parameter with name 'sort' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateSort<T>(Expression<Func<T, object>> sort, string message)
        {
            if (sort == null)
            {
                throw new ArgumentNullException(paramName: nameof(sort), message: message);
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'projection' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for projection expression.</typeparam>
        /// <typeparam name="TResult">Type of the output object for projection expression.</typeparam>
        /// <param name="projection">Dummy parameter with name 'projection' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateProjection<T, TResult>(Expression<Func<T, TResult>> projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(paramName: nameof(projection));
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'projection' if it is null and throws ArgumentNullException.
        /// </summary>
        /// <typeparam name="T">Type of the input object for projection expression.</typeparam>
        /// <typeparam name="TResult">Type of the output object for projection expression.</typeparam>
        /// <param name="projection">Dummy parameter with name 'projection' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateProjection<T, TResult>(Expression<Func<T, TResult>> projection, string message)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(paramName: nameof(projection), message: message);
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'skip' is in allowed range and throws InvalidSkipValuePagingException.
        /// </summary>
        /// <param name="skip">Dummy parameter with name 'skip' to be checked.</param>
        /// <exception cref="InvalidSkipValuePagingException"></exception>
        public static void ValidateSkip(int skip)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException();
            }
        }

        /// <summary>
        /// Checks dummy parameter with name 'skip' is in allowed range and throws InvalidSkipValuePagingException.
        /// </summary>
        /// <param name="skip">Dummy parameter with name 'skip' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant InvalidSkipValuePagingException.</param>
        /// <exception cref="InvalidSkipValuePagingException"></exception>
        public static void ValidateSkip(int skip, string message)
        {
            if (skip < 0)
            {
                throw new InvalidSkipValuePagingException(message: message);
            }
        }

        /// <summary>
        /// Checks if a dummy parameter with name 'take' is in allowed range and throws InvalidTakeValuePagingException.
        /// </summary>
        /// <param name="take">Dummy parameter with name 'take' to be checked.</param>
        /// <exception cref="InvalidTakeValuePagingException"></exception>
        public static void ValidateTake(int take)
        {
            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException();
            }
        }

        /// <summary>
        /// Checks if a dummy parameter with name 'take' is in allowed range and throws InvalidTakeValuePagingException.
        /// </summary>
        /// <param name="take">Dummy parameter with name 'take' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant InvalidTakeValuePagingException.</param>
        /// <exception cref="InvalidTakeValuePagingException"></exception>
        public static void ValidateTake(int take, string message)
        {
            if (1 > take || take > PagingConstants.MaximalItemsPerPageAllowed)
            {
                throw new InvalidTakeValuePagingException(message: message);
            }
        }

        /// <summary>
        /// Checks if a dummy parameter with name 'fileName' is null or whitespace and throws ArgumentNullException.
        /// </summary>
        /// <param name="fileName">Dummy parameter with name 'fileName' to be checked.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(paramName: nameof(fileName));
            }
        }

        /// <summary>
        /// Checks if a dummy parameter with name 'fileName' is null or whitespace and throws ArgumentNullException.
        /// </summary>
        /// <param name="fileName">Dummy parameter with name 'fileName' to be checked.</param>
        /// <param name="message">Custom message to be added in the resultant ArgumentNullException.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateFileName(string fileName, string message)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(paramName: nameof(fileName), message: message);
            }
        }
    }
}
