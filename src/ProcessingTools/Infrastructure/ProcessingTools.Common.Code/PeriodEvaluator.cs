// <copyright file="PeriodEvaluator.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code
{
    using System;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Period evaluator.
    /// </summary>
    public class PeriodEvaluator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodEvaluator"/> class.
        /// </summary>
        /// <param name="periodType">Period type.</param>
        /// <param name="now">Referent date.</param>
        public PeriodEvaluator(FilterMonthPeriodType periodType, DateTime now)
        {
            FilterPeriodType mType = periodType == FilterMonthPeriodType.LastMonth ? FilterPeriodType.LastMonth : FilterPeriodType.ThisMonth;
            this.Evaluate(mType, now);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodEvaluator"/> class.
        /// </summary>
        /// <param name="periodType">Period type.</param>
        /// <param name="now">Referent date.</param>
        public PeriodEvaluator(FilterPeriodType periodType, DateTime now)
        {
            this.Evaluate(periodType, now);
        }

        /// <summary>
        /// Gets the from date.
        /// </summary>
        public DateTime FromDate { get; private set; }

        /// <summary>
        /// Gets the to date.
        /// </summary>
        public DateTime ToDate { get; private set; }

        /// <summary>
        /// Gets or sets the period type.
        /// </summary>
        public FilterPeriodType PeriodType { get; set; }

        /// <summary>
        /// Evaluates the dates by provided referent date and period type.
        /// </summary>
        /// <param name="periodType">Period type.</param>
        /// <param name="now">Referent date.</param>
        public void Evaluate(FilterPeriodType periodType, DateTime now)
        {
            var today = now.Date;

            switch (periodType)
            {
                case FilterPeriodType.Yesterday:
                    this.FromDate = today.AddDays(-1);
                    this.ToDate = today;
                    break;

                case FilterPeriodType.ThisWeek:
                    this.FromDate = today.GetFirstDayOfWeek();
                    this.ToDate = today.GetLastDayOfWeek();
                    break;

                case FilterPeriodType.LastWeek:
                    this.FromDate = today.GetFirstDayOfWeek().AddDays(-7);
                    this.ToDate = this.FromDate.AddDays(7);
                    break;

                case FilterPeriodType.ThisMonth:
                    this.FromDate = today.GetFirstDayOfMonth();
                    this.ToDate = this.FromDate.AddMonths(1);
                    break;

                case FilterPeriodType.LastMonth:
                    this.FromDate = today.GetFirstDayOfMonth().AddMonths(-1);
                    this.ToDate = this.FromDate.AddMonths(1);
                    break;

                ////case FilterPeriodType.Today:
                default:
                    this.FromDate = today;
                    this.ToDate = this.FromDate.AddDays(1);
                    break;
            }
        }
    }
}
