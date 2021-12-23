// <copyright file="Signature.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Signature.
    /// </summary>
    internal class Signature : IEquatable<Signature>
    {
        private readonly int hashCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public Signature(IEnumerable<DynamicProperty> properties)
        {
            this.Properties = properties.ToArray();
            this.hashCode = 0;
            foreach (var p in properties)
            {
                this.hashCode ^= p.Name.GetHashCode(StringComparison.InvariantCulture) ^ p.Type.GetHashCode();
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public DynamicProperty[] Properties { get; }

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
            if (this.Properties.Length != other.Properties.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Properties.Length; i++)
            {
                if (
                    this.Properties[i].Name != other.Properties[i].Name ||
                    this.Properties[i].Type != other.Properties[i].Type)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
