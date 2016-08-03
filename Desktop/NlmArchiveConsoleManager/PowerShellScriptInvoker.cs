namespace ProcessingTools.NlmArchiveConsoleManager
{
    using System;
    using System.Collections.ObjectModel;
    using System.Management.Automation;
    using System.Threading;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public class PowerShellScriptInvoker
    {
        private readonly ILogger logger;

        public PowerShellScriptInvoker()
            : this(null)
        {
        }

        public PowerShellScriptInvoker(ILogger logger)
        {
            this.logger = logger;
        }

        public void Invoke(string script, params PowerShellScriptParameter[] parameters)
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript(script);

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        powerShellInstance.AddParameter(parameter.Name, parameter.Value);
                    }
                }

                // invoke execution on the pipeline (collecting output)
                var powerShellOutput = powerShellInstance.Invoke();

                foreach (var outputItem in powerShellOutput)
                {
                    if (outputItem != null)
                    {
                        this.logger?.Log(outputItem.BaseObject.GetType().FullName);
                        this.logger?.Log(outputItem.BaseObject.ToString() + "\n");
                    }
                }

                if (powerShellInstance.Streams.Debug.Count > 0)
                {
                    foreach (var record in powerShellInstance.Streams.Debug)
                    {
                        this.logger?.Log(record.Message);
                        this.logger?.Log(record.InvocationInfo);
                        this.logger?.Log(record.PipelineIterationInfo);
                    }
                }

                ////// TODO: Powershell in Win10
                ////if (powerShellInstance.Streams.Information.Count > 0)
                ////{
                ////    foreach (var record in powerShellInstance.Streams.Information)
                ////    {
                ////        this.logger?.Log(LogType.Info, record.ToString());
                ////    }
                ////}

                if (powerShellInstance.Streams.Warning.Count > 0)
                {
                    foreach (var record in powerShellInstance.Streams.Warning)
                    {
                        this.logger?.Log(LogType.Warning, record.Message);
                    }
                }

                if (powerShellInstance.Streams.Error.Count > 0)
                {
                    foreach (var record in powerShellInstance.Streams.Error)
                    {
                        this.logger?.Log(LogType.Error, record.ErrorDetails.Message);
                    }
                }
            }
        }

        public void Invoke()
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                // use "AddScript" to add the contents of a script file to the end of the execution pipeline.
                // use "AddCommand" to add individual commands/cmdlets to the end of the execution pipeline.
                powerShellInstance.AddScript("param($param1) $d = get-date; $s = 'test string value'; " +
                        "$d; $s; $param1; get-service");

                // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
                powerShellInstance.AddParameter("param1", "parameter 1 value!");

                // invoke execution on the pipeline (ignore output)
                powerShellInstance.Invoke();

                // invoke execution on the pipeline (collecting output)
                Collection<PSObject> powerShellOutput = powerShellInstance.Invoke();

                // check the other output streams (for example, the error stream)
                if (powerShellInstance.Streams.Error.Count > 0)
                {
                    // error records were written to the error stream.
                    // do something with the items found.
                }

                // loop through each output object item
                foreach (PSObject outputItem in powerShellOutput)
                {
                    // if null object was dumped to the pipeline during the script then a null
                    // object may be present here. check for null to prevent potential NRE.
                    if (outputItem != null)
                    {
                        // TODO: do something with the output item
                        // outputItem.BaseOBject
                        Console.WriteLine(outputItem.BaseObject.GetType().FullName);
                        Console.WriteLine(outputItem.BaseObject.ToString() + "\n");
                    }
                }
            }
        }

        public void InvokeAsync()
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                // this script has a sleep in it to simulate a long running script
                powerShellInstance.AddScript("start-sleep -s 7; get-service");

                // begin invoke execution on the pipeline
                IAsyncResult result = powerShellInstance.BeginInvoke();

                // do something else until execution has completed.
                // this could be sleep/wait, or perhaps some other work
                while (result.IsCompleted == false)
                {
                    Console.WriteLine("Waiting for pipeline to finish...");
                    Thread.Sleep(1000);

                    // might want to place a timeout here...
                }

                Console.WriteLine("Finished!");
            }
        }

        /// <summary>
        /// Sample execution scenario 2: Asynchronous
        /// </summary>
        /// <remarks>
        /// Executes a PowerShell script asynchronously with script output and event handling.
        /// </remarks>
        public void ExecuteAsynchronously()
        {
            using (PowerShell powerShellInstance = PowerShell.Create())
            {
                // this script has a sleep in it to simulate a long running script
                powerShellInstance.AddScript("$s1 = 'test1'; $s2 = 'test2'; $s1; write-error 'some error';start-sleep -s 7; $s2");

                // prepare a new collection to store output stream objects
                PSDataCollection<PSObject> outputCollection = new PSDataCollection<PSObject>();
                outputCollection.DataAdded += this.OutputCollection_DataAdded;

                // the streams (Error, Debug, Progress, etc) are available on the PowerShell instance.
                // we can review them during or after execution.
                // we can also be notified when a new item is written to the stream (like this):
                powerShellInstance.Streams.Error.DataAdded += this.Error_DataAdded;

                // begin invoke execution on the pipeline
                // use this overload to specify an output stream buffer
                IAsyncResult result = powerShellInstance.BeginInvoke<PSObject, PSObject>(null, outputCollection);

                // do something else until execution has completed.
                // this could be sleep/wait, or perhaps some other work
                while (result.IsCompleted == false)
                {
                    Console.WriteLine("Waiting for pipeline to finish...");
                    Thread.Sleep(1000);

                    // might want to place a timeout here...
                }

                Console.WriteLine("Execution has stopped. The pipeline state: " + powerShellInstance.InvocationStateInfo.State);

                foreach (PSObject outputItem in outputCollection)
                {
                    // TODO: handle/process the output items if required
                    Console.WriteLine(outputItem.BaseObject.ToString());
                }
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
            Console.WriteLine("Object added to output.");
        }

        /// <summary>
        /// Event handler for when Data is added to the Error stream.
        /// </summary>
        /// <param name="sender">Contains the complete PSDataCollection of all error output items.</param>
        /// <param name="e">Contains the index ID of the added collection item and the ID of the PowerShell instance this event belongs to.</param>
        private void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            // do something when an error is written to the error stream
            Console.WriteLine("An error was written to the Error stream!");
        }
    }
}
