namespace ProcessingTools.Globals
{
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class Diagnostics
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame[] sf = st.GetFrames();

            string stackPath = string.Empty;
            for (int i = sf.Length - 1; i > 1; i--)
            {
                stackPath += sf[i].GetMethod().Name + ".";
            }

            stackPath += sf[1].GetMethod().Name;

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
