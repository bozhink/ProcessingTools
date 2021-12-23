// <copyright file="AsyncLinqExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Async Linq extensions.
    /// </summary>
    public static class AsyncLinqExtensions
    {
        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence that contains elements to be counted.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        public static Task<int> CountAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Count());
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        public static Task<int> CountAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Count(predicate));
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Count());
        }

        /// <summary>
        /// Returns the number of elements in the specified sequence that satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Count(predicate));
        }

        /// <summary>
        /// Returns the first element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source"> The <see cref="IEnumerable{T}"/> to return the first element of.</param>
        /// <returns>The first element in the specified sequence.</returns>
        public static Task<TSource> FirstAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.First());
        }

        /// <summary>
        /// Returns the first element in a sequence that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source"> The <see cref="IEnumerable{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
        public static Task<TSource> FirstAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.First(predicate));
        }

        /// <summary>
        /// Returns the first element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> to return the first element of.</param>
        /// <returns>The first element in source.</returns>
        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.First());
        }

        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The first element in source that passes the test in predicate.</returns>
        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.First(predicate));
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to return the first element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.FirstOrDefault());
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(TSource) if source is empty or if no element passes the test specified by predicate; otherwise, the first element in source that passes the test specified by predicate.</returns>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.FirstOrDefault(predicate));
        }

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> to return the first element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.FirstOrDefault());
        }

        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}"/> to return the first element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(TSource) if source is empty or if no element passes the test specified by predicate; otherwise, the first element in source that passes the test specified by predicate.</returns>
        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.FirstOrDefault(predicate));
        }

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the last element of.</param>
        /// <returns>The value at the last position in the source sequence.</returns>
        public static Task<TSource> LastAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Last());
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
        public static Task<TSource> LastAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Last(predicate));
        }

        /// <summary>
        /// Returns the last element in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the last element of.</param>
        /// <returns>The value at the last position in source.</returns>
        public static Task<TSource> LastAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Last());
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The last element in source that passes the test specified by predicate.</returns>
        public static Task<TSource> LastAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Last(predicate));
        }

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the last element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the last element in source.</returns>
        public static Task<TSource> LastOrDefaultAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.LastOrDefault());
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(TSource) if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
        public static Task<TSource> LastOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.LastOrDefault(predicate));
        }

        /// <summary>
        /// Returns the last element in a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the last element of.</param>
        /// <returns>default(TSource) if source is empty; otherwise, the last element in source.</returns>
        public static Task<TSource> LastOrDefaultAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.LastOrDefault());
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the last element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>default(TSource) if source is empty or if no elements pass the test in the predicate function; otherwise, the last element of source that passes the test in the predicate function.</returns>
        public static Task<TSource> LastOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.LastOrDefault(predicate));
        }

        /// <summary>
        /// Returns an <see cref="long"/> that represents the total number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the source sequence.</returns>
        public static Task<long> LongCountAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.LongCount());
        }

        /// <summary>
        /// Returns an <see cref="long"/> that represents how many elements in a sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        public static Task<long> LongCountAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.LongCount(predicate));
        }

        /// <summary>
        /// Returns an <see cref="long"/> that represents the total number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the source sequence.</returns>
        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.LongCount());
        }

        /// <summary>
        /// Returns an <see cref="long"/> that represents the number of elements in a sequence that satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The number of elements in source that satisfy the condition in the predicate function.</returns>
        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.LongCount(predicate));
        }

        /// <summary>
        /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <returns>The single element of the input sequence.</returns>
        public static Task<TSource> SingleAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Single());
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence that satisfies the condition in predicate.</returns>
        public static Task<TSource> SingleAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Single(predicate));
        }

        /// <summary>
        /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the single element of.</param>
        /// <returns>The single element of the input sequence.</returns>
        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.Single());
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence that satisfies the condition in predicate.</returns>
        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.Single(predicate));
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.SingleOrDefault());
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence that satisfies the condition, or default(TSource) if no such element is found.</returns>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.SingleOrDefault(predicate));
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the single element of.</param>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.SingleOrDefault());
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The single element of the input sequence that satisfies the condition in predicate, or default(TSource) if no such element is found.</returns>
        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return Task.Run(() => source.SingleOrDefault(predicate));
        }

        /// <summary>
        /// Creates an array from a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create an array from.</param>
        /// <returns>An array that contains the elements from the input sequence.</returns>
        public static Task<TSource[]> ToArrayAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.ToArray());
        }

        /// <summary>
        /// Creates a <see cref="List{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">The <see cref="List{T}"/> to create a <see cref="IEnumerable{T}"/> from.</param>
        /// <returns>A <see cref="List{T}"/> that contains elements from the input sequence.</returns>
        public static Task<List<TSource>> ToListAsync<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Task.Run(() => source.ToList());
        }
    }
}
