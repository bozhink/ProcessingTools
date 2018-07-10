// <copyright file="WeeklySchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Schedules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using ProcessingTools.Attributes.Tasks;
    using ProcessingTools.Enumerations.Tasks;
    using ProcessingTools.Tasks.Models.Contracts.Schedules;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Schedule with single execution.
    /// </summary>
    [ScheduleIdentifier(ScheduleType.Weekly)]
    public class WeeklySchedule : BaseSchedule, IWeeklySchedule
    {
        private const int NumberOfDaysInWeek = 7;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeeklySchedule"/> class.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="taskSchedule">Associated task schedule.</param>
        public WeeklySchedule(DateTime start, ITaskSchedule taskSchedule)
            : base(start, taskSchedule)
        {
        }

        /// <inheritdoc/>
        public bool Monday { get; set; }

        /// <inheritdoc/>
        public bool Tuesday { get; set; }

        /// <inheritdoc/>
        public bool Wednesday { get; set; }

        /// <inheritdoc/>
        public bool Thursday { get; set; }

        /// <inheritdoc/>
        public bool Friday { get; set; }

        /// <inheritdoc/>
        public bool Saturday { get; set; }

        /// <inheritdoc/>
        public bool Sunday { get; set; }

        /// <inheritdoc/>
        public int RepeatPeriodInWeeks { get; set; }

        /// <inheritdoc/>
        public override void ReadFromData()
        {
            var data = JsonConvert.DeserializeObject<WeeklySchedule>(this.TaskSchedule.Data);

            this.Start = data.Start;
            this.RepeatInDay = data.RepeatInDay;
            this.FromTime = data.FromTime;
            this.ToTime = data.ToTime;
            this.MinutesToRepeat = data.MinutesToRepeat;
            this.StopDailyRepetitionOnSuccess = data.StopDailyRepetitionOnSuccess;

            this.RepeatPeriodInWeeks = data.RepeatPeriodInWeeks;

            this.Monday = data.Monday;
            this.Tuesday = data.Tuesday;
            this.Wednesday = data.Wednesday;
            this.Thursday = data.Thursday;
            this.Friday = data.Friday;
            this.Saturday = data.Saturday;
            this.Sunday = data.Sunday;
        }

        /// <inheritdoc/>
        public override void WriteToData()
        {
            this.TaskSchedule.Data = JsonConvert.SerializeObject(this);
        }

        /// <inheritdoc/>
        public override DateTime GetNextRunTime(bool taskSuccessFlag)
        {
            if (this.RepeatPeriodInWeeks < 1)
            {
                throw new InvalidOperationException("Period of repetition in weeks have to be grater than 0.");
            }

            var weekValues = new Dictionary<int, bool>
            {
                { 1, this.Monday },
                { 2, this.Tuesday },
                { 3, this.Wednesday },
                { 4, this.Thursday },
                { 5, this.Friday },
                { 6, this.Saturday },
                { 7, this.Sunday }
            };

            if (!weekValues.ContainsValue(true))
            {
                throw new InvalidOperationException("There are no selected execution days.");
            }

            var now = this.DateTimeProvider.Invoke();

            return this.GetNextRunTime(taskSuccessFlag, weekValues, now);
        }

        private DateTime GetNextRunTime(bool taskSuccessFlag, Dictionary<int, bool> weekValues, DateTime now)
        {
            var start = this.Start;
            if (now <= start)
            {
                return start;
            }

            int cycleLength = NumberOfDaysInWeek * this.RepeatPeriodInWeeks;

            var delta = now - start;
            int numberOfCycles = delta.Days / cycleLength;
            int daysOfCycles = numberOfCycles * cycleLength;

            var fromDateTime = this.EvaluateFromTime(start);
            var referenceDate = this.GetStartDayOfWeek(fromDateTime.AddDays(daysOfCycles));
            var checkDate = now.AddDays(cycleLength);
            while (referenceDate < checkDate)
            {
                if (this.AreInSameWeek(now, referenceDate))
                {
                    int dayOfWeek = this.GetDayOfWeek(now);
                    var result = referenceDate.AddDays(dayOfWeek - 1);

                    for (int i = dayOfWeek; i <= NumberOfDaysInWeek; ++i)
                    {
                        if (weekValues[i] && now.Date <= result.Date)
                        {
                            if (this.RepeatInDay && result.Date == now.Date)
                            {
                                if (this.StopDailyRepetitionOnSuccess && taskSuccessFlag)
                                {
                                    return this.GetNextRunTime(false, weekValues, now.Date.AddDays(1));
                                }

                                var evaluatedTime = this.DoRepeatInDayCorrectorStep(result, now);
                                if (evaluatedTime != null && evaluatedTime.HasValue && evaluatedTime >= now)
                                {
                                    result = evaluatedTime.Value;
                                    return result;
                                }
                            }

                            if (now <= result)
                            {
                                return result;
                            }
                        }

                        result = result.AddDays(1);
                    }
                }
                else if (now < referenceDate)
                {
                    var dayOfWeek = weekValues.First(p => p.Value).Key;
                    var nextRun = referenceDate.AddDays(dayOfWeek - 1);

                    return nextRun;
                }

                referenceDate = referenceDate.AddDays(cycleLength);
            }

            throw new InvalidOperationException("Next execution time can not be calculated.");
        }

        private int GetDayOfWeek(DateTime date)
        {
            var result = (int)date.DayOfWeek;
            if (result == 0)
            {
                result = NumberOfDaysInWeek;
            }

            return result;
        }

        private bool AreInSameWeek(DateTime date, DateTime referenceDate)
        {
            var startDayOfWeek = this.GetStartDayOfWeek(referenceDate);
            var endDayOfWeek = this.GetEndDayOfWeek(referenceDate);

            return startDayOfWeek <= date && date <= endDayOfWeek;
        }

        private DateTime GetStartDayOfWeek(DateTime date)
        {
            var dayOfWeek = this.GetDayOfWeek(date);
            return date.AddDays(1 - dayOfWeek);
        }

        private DateTime GetEndDayOfWeek(DateTime date)
        {
            var dayOfWeek = this.GetDayOfWeek(date);
            return date.AddDays(NumberOfDaysInWeek - dayOfWeek);
        }
    }
}
