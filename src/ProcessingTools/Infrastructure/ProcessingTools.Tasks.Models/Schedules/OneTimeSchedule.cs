// <copyright file="OneTimeSchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Schedules
{
    using System;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Attributes.Tasks;
    using ProcessingTools.Enumerations.Tasks;
    using ProcessingTools.Tasks.Models.Contracts.Schedules;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Schedule with single execution.
    /// </summary>
    [ScheduleIdentifier(ScheduleType.OneTime)]
    public class OneTimeSchedule : BaseSchedule, IOneTimeSchedule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeSchedule"/> class.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="taskSchedule">Associated task schedule.</param>
        public OneTimeSchedule(DateTime start, ITaskSchedule taskSchedule)
            : base(start, taskSchedule)
        {
        }

        /// <inheritdoc/>
        [JsonIgnore]
        public override bool RepeatInDay => false;

        /// <inheritdoc/>
        [JsonIgnore]
        public override bool StopDailyRepetitionOnSuccess => true;

        /// <inheritdoc/>
        [JsonIgnore]
        public override DateTime? FromTime { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public override DateTime? ToTime { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public override int? MinutesToRepeat { get; set; }

        /// <inheritdoc/>
        public override DateTime GetNextRunTime(bool taskSuccessFlag) => this.Start;

        /// <inheritdoc/>
        public override void ReadFromData()
        {
            var data = JsonConvert.DeserializeObject<OneTimeSchedule>(this.TaskSchedule.Data);

            this.Start = data.Start;
        }

        /// <inheritdoc/>
        public override void WriteToData()
        {
            this.TaskSchedule.Data = JsonConvert.SerializeObject(this);
        }
    }
}
