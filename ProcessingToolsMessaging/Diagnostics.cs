using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingTools
{
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
            StackTrace st = new StackTrace();
            StackFrame[] sf = st.GetFrames();

            if (stackPosition < sf.Length)
            {
                return sf[stackPosition].GetMethod().Name;
            }
            else
            {
                return "Unknown method";
            }
        }
    }
}
