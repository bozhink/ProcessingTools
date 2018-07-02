// <copyright file="ILogger.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log empty message.
        /// </summary>
        void Log();

        /// <summary>
        /// Log specified message.
        /// </summary>
        /// <param name="message">Message to be logged</param>
        void Log(object message);

        /// <summary>
        /// Log message with format string.
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to format</param>
        void Log(string format, params object[] args);

        /// <summary>
        /// Log exception with specified message.
        /// </summary>
        /// <param name="exception">Exception to be logged</param>
        /// <param name="message">Log message</param>
        void Log(Exception exception, object message);

        /// <summary>
        /// Log exception with message with format string
        /// </summary>
        /// <param name="exception">Exception to be logged</param>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to format</param>
        void Log(Exception exception, string format, params object[] args);

        /// <summary>
        /// Log specified message.
        /// </summary>
        /// <param name="type">Type of log</param>
        /// <param name="message">Message to be logged</param>
        void Log(LogType type, object message);

        /// <summary>
        /// Log message with format string.
        /// </summary>
        /// <param name="type">Type of log</param>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to format</param>
        void Log(LogType type, string format, params object[] args);

        /// <summary>
        /// Log exception with specified message.
        /// </summary>
        /// <param name="type">Type of log</param>
        /// <param name="exception">Exception to be logged</param>
        /// <param name="message">Log message</param>
        void Log(LogType type, Exception exception, object message);

        /// <summary>
        /// Log exception with message with format string
        /// </summary>
        /// <param name="type">Type of log</param>
        /// <param name="exception">Exception to be logged</param>
        /// <param name="format">A composite format string</param>
        /// <param name="args">An array of objects to format</param>
        void Log(LogType type, Exception exception, string format, params object[] args);
    }
}
