﻿namespace ProcessingTools.Contracts
{
    using System;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
