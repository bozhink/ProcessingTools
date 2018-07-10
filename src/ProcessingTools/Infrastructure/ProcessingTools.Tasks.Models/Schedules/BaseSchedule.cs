// <copyright file="BaseSchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Schedules
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using ProcessingTools.Tasks.Models.Contracts.Schedules;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Base schedule.
    /// </summary>
    public abstract class BaseSchedule : ISchedule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSchedule"/> class.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="taskSchedule">Associated task schedule.</param>
        protected BaseSchedule(DateTime start, ITaskSchedule taskSchedule)
        {
            this.TaskSchedule = taskSchedule ?? throw new ArgumentNullException(nameof(taskSchedule));
            this.Start = start;
            this.DateTimeProvider = () => DateTime.Now;
        }

        /// <inheritdoc/>
        public ITaskSchedule TaskSchedule { get; }

        /// <inheritdoc/>
        public virtual DateTime Start { get; set; }

        /// <inheritdoc/>
        public virtual DateTime? FromTime { get; set; }

        /// <inheritdoc/>
        public virtual DateTime? ToTime { get; set; }

        /// <inheritdoc/>
        [Range(30, 24 * 60, ErrorMessage = "Value must be between {0} and {1} minutes.")]
        public virtual int? MinutesToRepeat { get; set; }

        /// <inheritdoc/>
        public virtual bool RepeatInDay { get; set; }

        /// <inheritdoc/>
        public virtual bool StopDailyRepetitionOnSuccess { get; set; }

        /// <summary>
        /// Gets or sets the time provider.
        /// </summary>
        [JsonIgnore]
        internal Func<DateTime> DateTimeProvider { get; set; }

        /// <inheritdoc/>
        public abstract DateTime GetNextRunTime(bool taskSuccessFlag);

        /// <inheritdoc/>
        public abstract void ReadFromData();

        /// <inheritdoc/>
        public abstract void WriteToData();

        /// <summary>
        /// Do repeat-in-day corrector step.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="now">Value for now.</param>
        /// <returns>Corrected value for next time.</returns>
        protected DateTime? DoRepeatInDayCorrectorStep(DateTime start, DateTime now)
        {
            if (!this.MinutesToRepeat.HasValue || start.Date != now.Date)
            {
                return null;
            }

            DateTime from = this.EvaluateFromTime(start);
            DateTime to = this.EvaluateToTime(start);

            if (from > to || from > now || now > to)
            {
                return null;
            }

            var delta = now - start;
            var cycleLength = this.MinutesToRepeat.Value;
            int numberOfCycles = ((delta.Hours * 60) + delta.Minutes) / cycleLength;

            var result = start.AddMinutes(numberOfCycles * cycleLength);
            while (result < now)
            {
                result = result.AddMinutes(cycleLength);
            }

            if (result > to)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Evaluates the "From" time from specified start time.
        /// </summary>
        /// <param name="start">Referent time.</param>
        /// <returns>Value of the "From" time.</returns>
        protected DateTime EvaluateFromTime(DateTime start)
        {
            if (this.FromTime.HasValue)
            {
                return new DateTime(
                    year: start.Year,
                    month: start.Month,
                    day: start.Day,
                    hour: this.FromTime.Value.Hour,
                    minute: this.FromTime.Value.Minute,
                    second: this.FromTime.Value.Second);
            }
            else
            {
                return start;
            }
        }

        /// <summary>
        /// Evaluates the "To" time from specified start time.
        /// </summary>
        /// <param name="start">Referent time.</param>
        /// <returns>Value of the "To" time.</returns>
        protected DateTime EvaluateToTime(DateTime start)
        {
            if (this.ToTime.HasValue)
            {
                return new DateTime(
                    year: start.Year,
                    month: start.Month,
                    day: start.Day,
                    hour: this.ToTime.Value.Hour,
                    minute: this.ToTime.Value.Minute,
                    second: this.ToTime.Value.Second);
            }
            else
            {
                return start;
            }
        }
    }
}
