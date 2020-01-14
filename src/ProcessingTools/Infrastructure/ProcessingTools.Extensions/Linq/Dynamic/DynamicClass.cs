// <copyright file="DynamicClass.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
{
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Dynamic class.
    /// </summary>
    public abstract class DynamicClass
    {
        /// <inheritdoc/>
        public override string ToString()
        {
            PropertyInfo[] props = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < props.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(props[i].Name);
                sb.Append("=");
                sb.Append(props[i].GetValue(this, null));
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
