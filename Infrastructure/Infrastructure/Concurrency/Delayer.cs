namespace ProcessingTools.Infrastructure.Concurrency
{
    using System;
    using System.Threading;

    public static class Delayer
    {
        private const int DefaultDelayTyime = 500; // ms

        /// <summary>
        /// Delay current thread by 500 ms.
        /// </summary>
        public static void Delay()
        {
            Thread.Sleep(DefaultDelayTyime);
        }

        /// <summary>
        /// Delay current thread by 'milliseconds' ms.
        /// </summary>
        /// <param name="milliseconds">Time to delay.</param>
        public static void Delay(int milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentException("Delay time should not be negative.", "milliseconds");
            }

            Thread.Sleep(milliseconds);
        }
    }
}