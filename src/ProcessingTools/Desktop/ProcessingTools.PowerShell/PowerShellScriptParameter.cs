// <copyright file="PowerShellScriptParameter.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.PowerShell
{
    /// <summary>
    /// PowerShell script parameter.
    /// </summary>
    public class PowerShellScriptParameter
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        public string Value { get; set; }
    }
}
