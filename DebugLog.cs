using System;
using System.Diagnostics;
using System.Text;

namespace DebugLogCalss
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
            try
            {
                MessageLog(messages);
            }
            catch { };

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
            try
            {
                DetailExceptionLog(e); // select
                SimpleExceptionLog(e);// select
            }
            catch { };

        }

        private static void MessageLog(params object[] messages)
        {
            string today = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
            StringBuilder result = new StringBuilder();
            result.Append("-> ");
            result.Append(today);
            result.Append(" ");
            result.Append("[ Log ]  : ");

            foreach (object msssage in messages)
            {
                result.Append(msssage);
                result.Append("  ");
            }
            result.Append("\n");
            System.Diagnostics.Debug.WriteLine(result);
        }


        private static void DetailExceptionLog(Exception e)
        {
            StackTrace stackTrace = new StackTrace(true);
            StringBuilder result = new StringBuilder();
            int count = Convert.ToInt32(stackTrace.FrameCount);
            string spaceCount = "";
            count = count > 5 ? 5 : count;
            for (int i = 0; i < count; i++)
            {
                StackFrame stackFrame = stackTrace.GetFrame(count - i);
                if (stackFrame is null ||
                  stackFrame.GetMethod().ToString().Equals("Void e(System.Exception)") ||
                  stackFrame.GetMethod().ToString().Equals("Void ExceptionLog(System.Exception)")) break;


                for (int ii = 1; ii < i; ii++)
                {
                    spaceCount += " ";
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
            string[] eStackTeace = (e.StackTrace).Split(new string[] { "\n" }, StringSplitOptions.None);
            result.Append("\n");
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


            for (int i = 0; i < eStackTeace.Length; i++)
            {
                result.Append(spaceCount);
                result.Append("   ");
                result.Append($"[  Stack {i}  ]  : ");
                result.Append(eStackTeace[i]);
                result.Append("\n");
            }

            System.Diagnostics.Debug.WriteLine(result);



        }
        private static void SimpleExceptionLog(Exception e)
        {
            StackTrace stackTrace = new StackTrace(true);
            StringBuilder result = new StringBuilder();
            int count = Convert.ToInt32(stackTrace.FrameCount);

            count = count > 5 ? 5 : count;

            string today = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
            result.Append("  ---- StackTrace Start ----   ");
            result.Append("\n");
            result.Append("[ Time ] : ");
            result.Append(today);


            for (int i = 0; i < count; i++)
            {
                StackFrame stackFrame = stackTrace.GetFrame(count - i);
                if (stackFrame is null ||
                  stackFrame.GetMethod().ToString().Equals("Void e(System.Exception)") ||
                  stackFrame.GetMethod().ToString().Equals("Void ExceptionLog(System.Exception)")) break;


                result.Append("\n");
                result.Append("[ StackTrace ]  : ");
                result.Append(GetSimpleData(stackFrame.GetFileName(), "" + stackFrame.GetMethod(), stackFrame.GetFileLineNumber()));


            }

            string eSignature = Convert.ToString(e.TargetSite);
            string eMessage = Convert.ToString(e.Message);
            string[] eStackTeace = (e.StackTrace).Split(new string[] { "\n" }, StringSplitOptions.None);
            result.Append("\n");
            result.Append("[ Cignature  ]  : ");
            result.Append(eSignature);
            result.Append("\n");

            result.Append("[  Message   ]  : ");
            result.Append(eMessage);
            result.Append("\n");

            result.Append("  ---- StackTrace in Stack ----   ");
            result.Append("\n");
            for (int i = 0; i < eStackTeace.Length; i++)
            {
                result.Append($" [ Stack {i} ]  : ");
                result.Append(eStackTeace[i]);
                result.Append("\n");
            }
            result.Append("  ---- StackTrace End ----   ");
            System.Diagnostics.Debug.WriteLine(result);



        }

        private static string GetSimpleData(string fullFile, string fullMethod, int line)
        {
            int classLocation = fullFile.LastIndexOf("\\");
            string FileName = fullFile.Substring(classLocation + 1);

            int MethodLocationStart = fullMethod.IndexOf(" ");
            string Method = fullMethod.Substring(MethodLocationStart);

            StringBuilder result = new StringBuilder();
            result.Append(FileName);
            result.Append(" -> ");
            result.Append(Method);
            result.Append(" : ");
            result.Append(line);
            return result.ToString();
        }
    }


}
