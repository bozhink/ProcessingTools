using System;

namespace Base
{
    public class Timer
    {
        private int tic, toc;

        public Timer()
        {
            this.tic = Gettime();
            this.toc = this.tic;
        }

        public int Tic
        {
            get
            {
                return this.tic;
            }
        }

        public int Toc
        {
            get
            {
                return this.toc;
            }
        }

        public int Interval
        {
            get
            {
                this.toc = Gettime();
                return this.toc - this.tic;
            }
        }

        public void Start()
        {
            this.tic = Gettime();
            this.toc = this.tic;
        }

        public void Stop()
        {
            this.toc = Gettime();
        }

        public void WriteOutput()
        {
            this.toc = Gettime();
            int interval = this.toc - this.tic;
            Alert.Message("Completed in " + interval + " ms = " + (interval / 1000.0) + " s = " + (interval / 60000.0) + " m");
        }

        private static int Gettime()
        {
            DateTime t = DateTime.Now;
            return t.Millisecond + (1000 * (t.Second + (60 * (t.Minute + (60 * t.Hour)))));
        }
    }
}
