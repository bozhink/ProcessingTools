// <copyright file="PowerShellScriptInvoker.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.PowerShell
{
    using System;
    using System.Management.Automation;
    using System.Threading;
    using Microsoft.Extensions.Logging;
    using PowerShell = System.Management.Automation.PowerShell;

    /// <summary>
    /// PowerShell script invoker.
    /// </summary>
    public class PowerShellScriptInvoker
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerShellScriptInvoker"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        public PowerShellScriptInvoker(ILogger<PowerShellScriptInvoker> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Invoke script with parameters.
        /// </summary>
        /// <param name="script">Script command to be invoked.</param>
        /// <param name="parameters">Parameters for the command.</param>
        public void Invoke(string script, params PowerShellScriptParameter[] parameters)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        powerShell.AddParameter(parameter.Name, parameter.Value);
                    }
                }

                // invoke execution on the pipeline (collecting output)
                var output = powerShell.Invoke();

                foreach (var outputItem in output)
                {
                    if (outputItem != null)
                    {
                        this.logger.LogDebug(outputItem.BaseObject.GetType().FullName);
                        this.logger.LogDebug(outputItem.BaseObject.ToString() + "\n");
                    }
                }

                this.ProcessPowerShellStreams(powerShell);
            }
        }

        /// <summary>
        /// Invoke script with parameters.
        /// </summary>
        /// <param name="script">Script command to be invoked.</param>
        /// <param name="parameters">Parameters for the command.</param>
        public void InvokeAsync(string script, params PowerShellScriptParameter[] parameters)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        powerShell.AddParameter(parameter.Name, parameter.Value);
                    }
                }

                // begin invoke execution on the pipeline
                IAsyncResult result = powerShell.BeginInvoke();

                // do something else until execution has completed.
                // this could be sleep/wait, or perhaps some other work
                while (!result.IsCompleted)
                {
                    Thread.Sleep(1000);
                }

                this.logger.LogDebug("Execution has stopped. The pipeline state: " + powerShell.InvocationStateInfo.State);
            }
        }

        /// <summary>
        /// Sample execution scenario 2: Asynchronous
        /// </summary>
        /// <param name="script">Script command to be invoked.</param>
        /// <param name="parameters">Parameters for the command.</param>
        /// <remarks>
        /// Executes a PowerShell script asynchronously with script output and event handling.
        /// </remarks>
        public void ExecuteAsynchronously(string script, params PowerShellScriptParameter[] parameters)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        powerShell.AddParameter(parameter.Name, parameter.Value);
                    }
                }

                // prepare a new collection to store output stream objects
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                outputCollection.DataAdded += this.OutputCollection_DataAdded;

                // the streams (Error, Debug, Progress, etc) are available on the PowerShell instance.
                // we can review them during or after execution.
                // we can also be notified when a new item is written to the stream (like this):
                powerShell.Streams.Error.DataAdded += this.Error_DataAdded;

                // begin invoke execution on the pipeline
                // use this overload to specify an output stream buffer
                IAsyncResult result = powerShell.BeginInvoke<PSObject, PSObject>(null, outputCollection);

                // do something else until execution has completed.
                // this could be sleep/wait, or perhaps some other work
                while (!result.IsCompleted)
                {
                    Thread.Sleep(1000);
                }

                this.logger.LogDebug("Execution has stopped. The pipeline state: " + powerShell.InvocationStateInfo.State);

                foreach (var outputItem in outputCollection)
                {
                    if (outputItem != null)
                    {
                        this.logger.LogDebug(outputItem.BaseObject.GetType().FullName);
                        this.logger.LogDebug(outputItem.BaseObject.ToString() + "\n");
                    }
                }

                this.ProcessPowerShellStreams(powerShell);
            }
        }

        /// <summary>
        /// Event handler for when data is added to the output stream.
        /// </summary>
        /// <param name="sender">Contains the complete PSDataCollection of all output items.</param>
        /// <param name="e">Contains the index ID of the added collection item and the ID of the PowerShell instance this event belongs to.</param>
        private void OutputCollection_DataAdded(object sender, DataAddedEventArgs e)
        {
            // do something when an object is written to the output stream
            this.logger.LogDebug("Object added to output.");
        }

        /// <summary>
        /// Event handler for when Data is added to the Error stream.
        /// </summary>
        /// <param name="sender">Contains the complete PSDataCollection of all error output items.</param>
        /// <param name="e">Contains the index ID of the added collection item and the ID of the PowerShell instance this event belongs to.</param>
        private void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            // do something when an error is written to the error stream
            this.logger.LogDebug("An error was written to the Error stream!");
        }

        private void ProcessPowerShellStreams(PowerShell powerShell)
        {
            if (powerShell.Streams.Debug.Count > 0)
            {
                foreach (var record in powerShell.Streams.Debug)
                {
                    this.logger.LogDebug(record.Message);
                    this.logger.LogDebug(record.InvocationInfo.ToString());
                    this.logger.LogDebug(record.PipelineIterationInfo.ToString());
                }
            }

            if (powerShell.Streams.Information.Count > 0)
            {
                foreach (var record in powerShell.Streams.Information)
                {
                    this.logger.LogInformation(record.ToString());
                }
            }

            if (powerShell.Streams.Warning.Count > 0)
            {
                foreach (var record in powerShell.Streams.Warning)
                {
                    this.logger.LogWarning(record.Message);
                }
            }

            if (powerShell.Streams.Error.Count > 0)
            {
                foreach (var record in powerShell.Streams.Error)
                {
                    this.logger.LogError(record.ErrorDetails?.Message ?? record.ToString());
                }
            }
        }
    }
}
