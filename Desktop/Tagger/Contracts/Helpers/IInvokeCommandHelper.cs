﻿namespace ProcessingTools.Tagger.Contracts.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using Commands;

    public interface IInvokeCommandHelper
    {
        ICollection<Task> Tasks { get; }

        Task<object> Invoke(string message, Func<Task> action);

        Task<object> Invoke<TCommand>(XmlNode context) where TCommand : ITaggerCommand;

        Task<object> Invoke(Type commandType, XmlNode context);
    }
}