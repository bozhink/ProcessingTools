// <copyright file="PowerShellHelper.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.PowerShell
{
    using System;
    using Microsoft.Extensions.Logging;
    using PowerShell = System.Management.Automation.PowerShell;

    /// <summary>
    /// PowerShell helper.
    /// </summary>
    public class PowerShellHelper
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellHelper"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public PowerShellHelper(ILogger<PowerShellHelper> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executes PowerShell command.
        /// </summary>
        /// <param name="command">PowerShell command to be executed.</param>
        public void Execute(string command)
        {
            using (var ps = PowerShell.Create())
            {
                var results = ps.AddScript(command).Invoke();
                foreach (var result in results)
                {
                    this.logger.LogDebug(result.ToString());
                }
            }
        }
    }
}
