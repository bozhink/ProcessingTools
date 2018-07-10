namespace ProcessingTools.Tasks.Models.Schedules
{
    using System;
    using Newtonsoft.Json;
    using ProcessingTools.Attributes.Tasks;
    using ProcessingTools.Enumerations.Tasks;
    using ProcessingTools.Tasks.Models.Contracts.Schedules;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Schedule with daily execution.
    /// </summary>
    [ScheduleIdentifier(ScheduleType.Daily)]
    public class DailySchedule : BaseSchedule, IDailySchedule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailySchedule"/> class.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="taskSchedule">Associated task schedule.</param>
        public DailySchedule(DateTime start, ITaskSchedule taskSchedule)
            : base(start, taskSchedule)
        {
        }

        /// <inheritdoc/>
        public int RepeatPeriodInDays { get; set; }

        /// <inheritdoc/>
        public override void ReadFromData()
        {
            var data = JsonConvert.DeserializeObject<DailySchedule>(this.TaskSchedule.Data);

            this.Start = data.Start;
            this.RepeatInDay = data.RepeatInDay;
            this.FromTime = data.FromTime;
            this.ToTime = data.ToTime;
            this.MinutesToRepeat = data.MinutesToRepeat;
            this.StopDailyRepetitionOnSuccess = data.StopDailyRepetitionOnSuccess;

            this.RepeatPeriodInDays = data.RepeatPeriodInDays;
        }

        /// <inheritdoc/>
        public override void WriteToData()
        {
            this.TaskSchedule.Data = JsonConvert.SerializeObject(this);
        }

        /// <inheritdoc/>
        public override DateTime GetNextRunTime(bool taskSuccessFlag)
        {
            if (this.RepeatPeriodInDays < 1)
            {
                throw new InvalidOperationException("Period of repetition have to be greater than 0.");
            }

            var now = this.DateTimeProvider.Invoke();

            return this.GetNextRunTime(taskSuccessFlag, now);
        }

        private DateTime GetNextRunTime(bool taskSuccessFlag, DateTime now)
        {
            var start = this.Start;
            if (now <= start)
            {
                return start;
            }

            DateTime result = this.EvaluateFromTime(start);

            var delta = now - start;
            var cycleLength = this.RepeatPeriodInDays;
            int numberOfCycles = delta.Days / cycleLength;

            result = result.AddDays(numberOfCycles * cycleLength);
            while (result.Date < now.Date)
            {
                result = result.AddDays(this.RepeatPeriodInDays);
            }

            if (this.RepeatInDay && result.Date == now.Date)
            {
                if (this.StopDailyRepetitionOnSuccess && taskSuccessFlag)
                {
                    return this.GetNextRunTime(false, now.Date.AddDays(1));
                }

                var evaluatedTime = this.DoRepeatInDayCorrectorStep(result, now);
                if (evaluatedTime.HasValue && evaluatedTime >= now)
                {
                    result = evaluatedTime.Value;
                    return result;
                }
            }

            if (result < now)
            {
                result = result.AddDays(this.RepeatPeriodInDays);
            }

            return result;
        }
    }
}
