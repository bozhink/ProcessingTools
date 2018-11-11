// <copyright file="Tracer.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Diagnostics
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// Tracer.
    /// </summary>
    public static class Tracer
    {
        /// <summary>
        /// Gets the current caller method.
        /// </summary>
        /// <returns>Name of the method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            StringBuilder stackPath = new StringBuilder();
            for (int i = stackFrames.Length - 1; i > 1; i--)
            {
                stackPath.Append(stackFrames[i].GetMethod().Name);
                stackPath.Append(".");
            }

            stackPath.Append(stackFrames[1].GetMethod().Name);

            return stackPath.ToString();
        }

        /// <summary>
        /// Gets the caller method at specified stack position.
        /// </summary>
        /// <param name="stackPosition">Stack position.</param>
        /// <returns>Name of the method.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(int stackPosition)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrame = stackTrace.GetFrames();

            if (stackPosition < stackFrame.Length)
            {
                return stackFrame[stackPosition].GetMethod().Name;
            }
            else
            {
                return "Unknown method";
            }
        }
    }
}
