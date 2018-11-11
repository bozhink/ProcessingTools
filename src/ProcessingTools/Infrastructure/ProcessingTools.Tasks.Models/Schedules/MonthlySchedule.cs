// <copyright file="MonthlySchedule.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Tasks.Models.Schedules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Attributes.Tasks;
    using ProcessingTools.Common.Enumerations.Tasks;
    using ProcessingTools.Tasks.Models.Contracts.Schedules;
    using ProcessingTools.Tasks.Models.Contracts.Tasks;

    /// <summary>
    /// Schedule with weekly execution.
    /// </summary>
    [ScheduleIdentifier(ScheduleType.OneTime)]
    public class MonthlySchedule : BaseSchedule, IMonthlySchedule
    {
        private const int NumberOfMonths = 12;
        private const int LastDayConstantConvantion = 32;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlySchedule"/> class.
        /// </summary>
        /// <param name="start">Referent start time.</param>
        /// <param name="taskSchedule">Associated task schedule.</param>
        public MonthlySchedule(DateTime start, ITaskSchedule taskSchedule)
            : base(start, taskSchedule)
        {
            this.Days = new HashSet<int>();
        }

        /// <inheritdoc/>
        public bool January { get; set; }

        /// <inheritdoc/>
        public bool February { get; set; }

        /// <inheritdoc/>
        public bool March { get; set; }

        /// <inheritdoc/>
        public bool April { get; set; }

        /// <inheritdoc/>
        public bool May { get; set; }

        /// <inheritdoc/>
        public bool June { get; set; }

        /// <inheritdoc/>
        public bool July { get; set; }

        /// <inheritdoc/>
        public bool August { get; set; }

        /// <inheritdoc/>
        public bool September { get; set; }

        /// <inheritdoc/>
        public bool October { get; set; }

        /// <inheritdoc/>
        public bool November { get; set; }

        /// <inheritdoc/>
        public bool December { get; set; }

        /// <inheritdoc/>
        public ICollection<int> Days { get; set; }

        /// <inheritdoc/>
        public override void ReadFromData()
        {
            var data = JsonConvert.DeserializeObject<MonthlySchedule>(this.TaskSchedule.Data);

            this.Start = data.Start;
            this.RepeatInDay = data.RepeatInDay;
            this.FromTime = data.FromTime;
            this.ToTime = data.ToTime;
            this.MinutesToRepeat = data.MinutesToRepeat;
            this.StopDailyRepetitionOnSuccess = data.StopDailyRepetitionOnSuccess;

            this.January = data.January;
            this.February = data.February;
            this.March = data.March;
            this.April = data.April;
            this.May = data.May;
            this.June = data.June;
            this.July = data.July;
            this.August = data.August;
            this.September = data.September;
            this.October = data.October;
            this.November = data.November;
            this.December = data.December;

            this.Days = data.Days;
        }

        /// <inheritdoc/>
        public override void WriteToData()
        {
            this.TaskSchedule.Data = JsonConvert.SerializeObject(this);
        }

        /// <inheritdoc/>
        public override DateTime GetNextRunTime(bool taskSuccessFlag)
        {
            var monthValues = new Dictionary<int, bool>
            {
                { 1, this.January },
                { 2, this.February },
                { 3, this.March },
                { 4, this.April },
                { 5, this.May },
                { 6, this.June },
                { 7, this.July },
                { 8, this.August },
                { 9, this.September },
                { 10, this.October },
                { 11, this.November },
                { 12, this.December }
            };

            if (!monthValues.ContainsValue(true))
            {
                throw new InvalidOperationException("There is no selected execution month.");
            }

            if (this.Days.Count < 1)
            {
                throw new InvalidOperationException("There is no selected execution days.");
            }

            if (!this.Days.All(d => d >= 1 && d <= LastDayConstantConvantion))
            {
                throw new InvalidOperationException("Invalid execution days are selected.");
            }

            var now = this.DateTimeProvider.Invoke();

            return this.GetNextRunTime(taskSuccessFlag, monthValues, now);
        }

        private DateTime GetNextRunTime(bool taskSuccessFlag, Dictionary<int, bool> monthValues, DateTime now)
        {
            var start = this.Start;
            if (now <= start)
            {
                return start;
            }

            DateTime fromDateTime = this.EvaluateFromTime(start);

            var orderedDays = this.Days.OrderBy(d => d).ToArray();

            // First case: this month execution
            if (monthValues[now.Month] && orderedDays.Any(d => d >= now.Day))
            {
                int year = now.Year;
                int month = now.Month;
                var days = orderedDays
                    .Where(day => day >= now.Day)
                    .Select(day => this.EnsureDay(year, month, day))
                    .Where(day => day >= 1)
                    .ToArray();

                foreach (var day in days)
                {
                    var result = new DateTime(
                        year: year,
                        month: month,
                        day: day,
                        hour: fromDateTime.Hour,
                        minute: fromDateTime.Minute,
                        second: fromDateTime.Second);

                    if (this.RepeatInDay && result.Date == now.Date)
                    {
                        if (this.StopDailyRepetitionOnSuccess && taskSuccessFlag)
                        {
                            return this.GetNextRunTime(false, monthValues, now.Date.AddDays(1));
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
            }

            // Check all months to the end of the current year
            for (int m = now.Month + 1; m <= NumberOfMonths; ++m)
            {
                if (monthValues[m])
                {
                    int year = now.Year;
                    int month = m;
                    int day = this.EnsureDay(year, month, orderedDays.First());

                    if (day >= 1)
                    {
                        return new DateTime(
                            year: year,
                            month: month,
                            day: day,
                            hour: fromDateTime.Hour,
                            minute: fromDateTime.Minute,
                            second: fromDateTime.Second);
                    }
                }
            }

            // Check months next year
            for (int m = 1; m <= now.Month; ++m)
            {
                if (monthValues[m])
                {
                    int year = now.Year + 1;
                    int month = m;
                    int day = this.EnsureDay(year, month, orderedDays.First());

                    if (day >= 1)
                    {
                        return new DateTime(
                            year: year,
                            month: month,
                            day: day,
                            hour: fromDateTime.Hour,
                            minute: fromDateTime.Minute,
                            second: fromDateTime.Second);
                    }
                }
            }

            throw new InvalidOperationException("Invalid data.");
        }

        private int EnsureDay(int year, int month, int day)
        {
            if (day < 1 || day > LastDayConstantConvantion)
            {
                return -1;
            }

            int monthLenght = this.GetMonthLength(year, month);
            if (day == LastDayConstantConvantion)
            {
                return monthLenght;
            }

            if (day > monthLenght)
            {
                // e.g. February 31
                return -1;
            }

            return day;
        }

        private int GetMonthLength(int year, int month)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;

                case 2:
                    return (year % 4 == 0) ? 29 : 28;

                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;

                default:
                    throw new ArgumentOutOfRangeException(nameof(month), "Invalid month number.");
            }
        }
    }
}
