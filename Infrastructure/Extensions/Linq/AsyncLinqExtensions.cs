namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public static class AsyncLinqExtensions
    {
        public static Task<int> CountAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.Count());

        public static Task<int> CountAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.Count(predicate));

        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.Count());

        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.Count(predicate));

        public static Task<TSource> FirstAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.First());

        public static Task<TSource> FirstAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.First(predicate));

        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.First());

        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.First(predicate));

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.FirstOrDefault());

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.FirstOrDefault(predicate));

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.FirstOrDefault());

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.FirstOrDefault(predicate));

        public static Task<TSource> LastAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.Last());

        public static Task<TSource> LastAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.Last(predicate));

        public static Task<TSource> LastAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.Last());

        public static Task<TSource> LastAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.Last(predicate));

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.LastOrDefault());

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.LastOrDefault(predicate));

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.LastOrDefault());

        public static Task<TSource> LastOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.LastOrDefault(predicate));

        public static Task<long> LongCountAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.LongCount());

        public static Task<long> LongCountAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.LongCount(predicate));

        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.LongCount());

        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.LongCount(predicate));

        public static Task<TSource> SingleAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.Single());

        public static Task<TSource> SingleAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.Single(predicate));

        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.Single());

        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.Single(predicate));

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.SingleOrDefault());

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) => Task.Run(() => source.SingleOrDefault(predicate));

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.SingleOrDefault());

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) => Task.Run(() => source.SingleOrDefault(predicate));

        public static Task<TSource[]> ToArrayAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.ToArray());

        public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.ToArray());

        public static Task<List<TSource>> ToListAsync<TSource>(this IEnumerable<TSource> source) => Task.Run(() => source.ToList());

        public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source) => Task.Run(() => source.ToList());
    }
}
