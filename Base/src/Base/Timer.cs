using System;

namespace Base
{
	public class Timer
	{
		private int tic, toc;

		public Timer()
		{
			tic = gettime();
			toc = tic;
		}

		public void Start()
		{
			tic = gettime();
			toc = tic;
		}

		public void Stop()
		{
			toc = gettime();
		}

		public int Tic
		{
			get { return tic; }
		}

		public int Toc
		{
			get { return toc; }
		}

		public int Interval
		{
			get { toc = gettime(); return toc - tic; }
		}

		public void WriteOutput()
		{
			toc = gettime();
			int interval = toc - tic;
			Alert.Message("Completed in " + interval + " ms = " + interval / 1000.0 + " s = " + interval / 60000.0 + " m");
		}

		private int gettime()
		{
			DateTime t = DateTime.Now;
			return t.Millisecond + 1000 * (t.Second + 60 * (t.Minute + 60 * t.Hour));
		}
	}
}
