namespace ProcessingTools.Extensions.Linq
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class AsyncLinqExtensions
    {
        public static Task<T> FirstAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.First());

        public static Task<T> FirstAsync<T>(this IQueryable<T> items) => Task.Run(() => items.First());

        public static Task<T> FirstOrDefaultAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.FirstOrDefault());

        public static Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> items) => Task.Run(() => items.FirstOrDefault());

        public static Task<T> LastAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.Last());

        public static Task<T> LastAsync<T>(this IQueryable<T> items) => Task.Run(() => items.Last());

        public static Task<T> LastOrDefaultAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.LastOrDefault());

        public static Task<T> LastOrDefaultAsync<T>(this IQueryable<T> items) => Task.Run(() => items.LastOrDefault());

        public static Task<T> SingleAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.Single());

        public static Task<T> SingleAsync<T>(this IQueryable<T> items) => Task.Run(() => items.Single());

        public static Task<T> SingleOrDefaultAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.SingleOrDefault());

        public static Task<T> SingleOrDefaultAsync<T>(this IQueryable<T> items) => Task.Run(() => items.SingleOrDefault());

        public static Task<T[]> ToArrayAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.ToArray());

        public static Task<T[]> ToArrayAsync<T>(this IQueryable<T> items) => Task.Run(() => items.ToArray());

        public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> items) => Task.Run(() => items.ToList());

        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> items) => Task.Run(() => items.ToList());
    }
}
