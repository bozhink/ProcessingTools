// <copyright file="CharacteristicFunctions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

// See https://www.codeproject.com/Articles/1160683/Functional-Programming-in-Csharp-2
namespace ProcessingTools.Common.Sets
{
    using System;

    /// <summary>
    /// Characteristic functions of sets.
    /// </summary>
    /// <remarks>
    /// The Characteristic Function S'(x) of the set S' is a function which associates either true or false with each element x of S.
    /// </remarks>
    public static class CharacteristicFunctions
    {
        /// <summary>
        /// Returns the characteristic function of the Empty set.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <returns>The characteristic function of the Empty set.</returns>
        public static Predicate<T> Empty<T>()
        {
            return x => false;
        }

        /// <summary>
        /// Returns the characteristic function of the whole set.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <returns>The characteristic function of the whole set.</returns>
        public static Predicate<T> All<T>()
        {
            return x => true;
        }

        /// <summary>
        /// Returns the characteristic function of the Singleton set.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">Element to be checked.</param>
        /// <returns>The characteristic function of the Singleton set.</returns>
        public static Predicate<T> Singleton<T>(T e)
        {
            return x => e.Equals(x);
        }

        /// <summary>
        /// Returns the characteristic function of the Union of two sets.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the Union of two sets.</returns>
        public static Predicate<T> Union<T>(this Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) || f(x);
        }

        /// <summary>
        /// Returns the characteristic function of the Intersection of two sets.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the Intersection of two sets.</returns>
        public static Predicate<T> Intersection<T>(this Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) && f(x);
        }

        /// <summary>
        /// Returns the characteristic function of the Cartesian Product of two sets.
        /// </summary>
        /// <typeparam name="T1">Type of the left set element.</typeparam>
        /// <typeparam name="T2">Type of the right set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the Cartesian Product of two sets.</returns>
        public static Func<T1, T2, bool> CartesianProduct<T1, T2>(this Predicate<T1> e, Predicate<T2> f)
        {
            return (x, y) => e(x) && f(y);
        }

        /// <summary>
        /// Returns the characteristic function of the Complement of two sets.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the Complement of two sets.</returns>
        public static Predicate<T> Complement<T>(this Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) && !f(x);
        }

        /// <summary>
        /// Returns the characteristic function of the symmetric difference without XOR of two sets.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the symmetric difference without XOR of two sets.</returns>
        public static Predicate<T> SymmetricDifferenceWithoutXor<T>(this Predicate<T> e, Predicate<T> f)
        {
            return Union(e.Complement(f), f.Complement(e));
        }

        /// <summary>
        /// Returns the characteristic function of the symmetric difference with XOR of two sets.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the left set.</param>
        /// <param name="f">The characteristic function of the right set.</param>
        /// <returns>The characteristic function of the symmetric difference with XOR of two sets.</returns>
        public static Predicate<T> SymmetricDifferenceWithXor<T>(this Predicate<T> e, Predicate<T> f)
        {
            return x => e(x) ^ f(x);
        }

        /// <summary>
        /// Checks whether a set contains a specified element.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="e">The characteristic function of the set.</param>
        /// <param name="x">Element to be checked.</param>
        /// <returns>Value indicating whether a set contains a specified element.</returns>
        public static bool Contains<T>(this Predicate<T> e, T x)
        {
            return e(x);
        }

        /// <summary>
        /// Adds an element to a set.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="s">The characteristic function of the set.</param>
        /// <param name="e">Element to be added.</param>
        /// <returns>The characteristic function of the new set.</returns>
        public static Predicate<T> Add<T>(this Predicate<T> s, T e)
        {
            return x => x.Equals(e) || s(x);
        }

        /// <summary>
        /// Removes an element from a set.
        /// </summary>
        /// <typeparam name="T">Type of the set element.</typeparam>
        /// <param name="s">The characteristic function of the set.</param>
        /// <param name="e">Element to be removed.</param>
        /// <returns>The characteristic function of the new set.</returns>
        public static Predicate<T> Remove<T>(this Predicate<T> s, T e)
        {
            return x => !x.Equals(e) && s(x);
        }
    }
}
