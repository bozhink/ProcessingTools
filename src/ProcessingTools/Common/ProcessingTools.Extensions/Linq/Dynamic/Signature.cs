// <copyright file="Signature.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Signature
    /// </summary>
    internal class Signature : IEquatable<Signature>
    {
        private readonly DynamicProperty[] properties;
        private readonly int hashCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.properties = properties.ToArray();
            this.hashCode = 0;
            foreach (var p in properties)
            {
                this.hashCode ^= p.Name.GetHashCode() ^ p.Type.GetHashCode();
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public DynamicProperty[] Properties => this.properties;

        /// <inheritdoc/>
        public override int GetHashCode() => this.hashCode;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Signature && this.Equals((Signature)obj);
        }

        /// <inheritdoc/>
        public bool Equals(Signature other)
        {
            if (this.properties.Length != other.properties.Length)
            {
                return false;
            }

            for (int i = 0; i < this.properties.Length; i++)
            {
                if (
                    this.properties[i].Name != other.properties[i].Name ||
                    this.properties[i].Type != other.properties[i].Type)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
