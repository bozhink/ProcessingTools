namespace ProcessingTools.Loggers.Diagnostics
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class Tracer
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            StringBuilder stackPath = new StringBuilder();
            for (int i = stackFrames.Length - 1; i > 1; i--)
            {
                stackPath.Append(stackFrames[i].GetMethod().Name);
                stackPath.Append(".");
            }

            stackPath.Append(stackFrames[1].GetMethod().Name);

            return stackPath.ToString();
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
