namespace ProcessingTools.Loggers.Diagnostics
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class Tracer
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            string stackPath = string.Empty;
            for (int i = stackFrames.Length - 1; i > 1; i--)
            {
                stackPath += stackFrames[i].GetMethod().Name + ".";
            }

            stackPath += stackFrames[1].GetMethod().Name;

            return stackPath;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod(int stackPosition)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrame = stackTrace.GetFrames();

            if (stackPosition < stackFrame.Length)
            {
                return stackFrame[stackPosition].GetMethod().Name;
            }
            else
            {
                return "Unknown method";
            }
        }
    }
}
