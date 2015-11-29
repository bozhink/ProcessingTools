namespace ProcessingTools.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IInternalResultBag<TResult, TException>
        where TException : Exception
    {
        ICollection<TResult> Results { get; set; }

        ICollection<TException> Exceptions { get; set; }
    }
}