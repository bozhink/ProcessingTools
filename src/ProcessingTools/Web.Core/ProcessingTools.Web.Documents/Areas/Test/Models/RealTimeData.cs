// <copyright file="RealTimeData.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Documents.Areas.Test.Models
{
    using System;

    /// <summary>
    /// Real time data.
    /// </summary>
    public class RealTimeData
    {
        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the data value.
        /// </summary>
        public double DataValue { get; set; }
    }
}
