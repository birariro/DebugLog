using System;
using System.Diagnostics;
using System.Text;

namespace Csharp_DebugLogDLL
{
    public class DebugLog
    {
        /// <summary>
        /// Debug mode shows logcat 
        /// Release mode doesn't show the logcat
        /// Print out the type of data you want with the parameter.
        /// </summary>
        /// <param name="messages">Any data type and free count</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void m(params object[] messages)
        {
            MessageLog(messages);
        }
        /// <summary>
        /// Debug mode shows logcat 
        /// Release mode doesn't show the logcat
        /// Add the exception as a parameter.
        /// </summary>
        /// <param name="e">data type Exception</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void e(Exception e)
        {
            ExceptionLog(e);
        }

        private static void MessageLog(params object[] messages)
        {


            StringBuilder result = new StringBuilder();
            result.Append("-> ");
            result.Append("[LogMessage]  : ");

            foreach (object msssage in messages)
            {
                result.Append(msssage);
                result.Append("  ");

            }
            result.Append("\n");
            Console.WriteLine(result);
        }


        private static void ExceptionLog(Exception e)
        {
            StackTrace stackTrace = new StackTrace(true);
            StringBuilder result = new StringBuilder();
            short count = Convert.ToByte(stackTrace.FrameCount);
            string spaceCount = "";
            count = count > (short)10 ? (short)10 : (short)count;
            for (int i = 1; i <= count - 2; i++)
            {
                StackFrame stackFrame = stackTrace.GetFrame(count - i);

                for (int ii = 1; ii < i; ii++)
                {
                    spaceCount += "  ";
                }
                result.Append("\n");
                result.Append(spaceCount);
                result.Append("-> ");
                result.Append("[ File Name ]  : ");
                result.Append(stackFrame.GetFileName());
                result.Append("\n");

                result.Append(spaceCount);
                result.Append("   ");
                result.Append("[Method Name]  : ");
                result.Append(stackFrame.GetMethod());
                result.Append("\n");

                result.Append(spaceCount);
                result.Append("   ");
                result.Append("[    Line   ]  : ");
                result.Append(stackFrame.GetFileLineNumber());
                result.Append("\n");


            }

            string eSignature = Convert.ToString(e.TargetSite);
            string eMessage = Convert.ToString(e.Message);

            result.Append(spaceCount);
            result.Append("   ");
            result.Append("[ Cignature ]  : ");
            result.Append(eSignature);
            result.Append("\n");

            result.Append(spaceCount);
            result.Append("   ");
            result.Append("[  Message  ]  : ");
            result.Append(eMessage);
            result.Append("\n");

            Console.WriteLine(result);

        }
    }
}
