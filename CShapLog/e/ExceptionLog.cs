using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShapLog.e
{
    public class ExceptionLog
    {
        static private ExceptionLog instance;
        private ExceptionLog()
        { }

        public static string Call(Exception e)
        {
            if (instance == null) instance = new ExceptionLog();
            return instance.Print(e);
        }
        private string Print(Exception e)
        {
            try
            {
                StackTrace stackTrace = new StackTrace(true);
                StringBuilder result = new StringBuilder();
                int count = Convert.ToInt32(stackTrace.FrameCount);

                count = count > 10 ? 10 : count;

                string today = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                result.Append("  ---- StackTrace Start ----   ");
                result.Append("\r\n");
                result.Append("[ Time ] : ");
                result.Append(today);


                for (int i = 0; i < count; i++)
                {
                    StackFrame stackFrame = stackTrace.GetFrame(count - i);
                    if (stackFrame is null ||
                      stackFrame.GetMethod().ToString().Contains("Void e") ||
                      stackFrame.GetMethod().ToString().Equals("Void ExceptionLog")) break;


                    result.Append("\r\n");
                    result.Append("[ StackTrace ]  : ");

                    result.Append(GetSimpleData(stackFrame.GetFileName(), "" + stackFrame.GetMethod(), stackFrame.GetFileLineNumber()));


                }


                string eSignature = Convert.ToString(e.TargetSite);
                string eMessage = Convert.ToString(e.Message);
                string[] eStackTeace = (e.StackTrace).Split(new string[] { "\r\n" }, StringSplitOptions.None);
                result.Append("\r\n");
                result.Append("[ Cignature  ]  : ");
                result.Append(eSignature);
                result.Append("\r\n");

                result.Append("[   Type     ]  : ");
                result.Append(e.GetType());
                result.Append("\r\n");

                result.Append("[  Message   ]  : ");
                result.Append(eMessage);
                result.Append("\r\n");
                result.Append("  ---- StackTrace in Stack ----   ");
                result.Append("\r\n");
                for (int i = 0; i < eStackTeace.Length; i++)
                {
                    result.Append($" [ Stack {i} ]  : ");
                    result.Append(GetSimpleTeace(eStackTeace[i]));
                    result.Append("\r\n");
                }
                result.Append("  ---- StackTrace End ----   ");

                


                return result.ToString();

            }
            catch (Exception LogE)
            {
                return LogE.Message;
            }
        }
        private static string GetSimpleData(string fullFile, string fullMethod, int line)
        {
            StringBuilder result = new StringBuilder();
            if (fullFile != null)
            {
                int classLocation = fullFile.LastIndexOf("/");
                string FileName = fullFile.Substring(classLocation + 1);

                classLocation = FileName.LastIndexOf("\\");
                FileName = FileName.Substring(classLocation + 1);
                result.Append(FileName);
            }
            else
            {
                result.Append("Empty");
            }
            if (fullMethod != null)
            {
                int MethodLocationStart = fullMethod.IndexOf(" ");
                string Method = fullMethod.Substring(MethodLocationStart);
                result.Append(" -> ");
                result.Append(Method);
            }


            result.Append(" : ");
            result.Append(line);
            return result.ToString();
        }
        private static string GetSimpleTeace(string fillTeace) //파일 경로를 지운다.
        {
            if (fillTeace.Contains(" in ") || fillTeace.Contains("\\")) // in 이라는 문구와 경로가있는경우
            {
                string[] terceSTr = fillTeace.Split(new string[] { " in " }, StringSplitOptions.None); // " in " 이라는 문자로 자른다.
                string startStr = terceSTr[0];
                string endStr = terceSTr[1]; // 마지막 경로 문자열을 가공할것이다.

                int endStrStartIndex = endStr.LastIndexOf("\\"); //마지막 경로를 찾는다.
                endStr = endStr.Substring(endStrStartIndex + 1);

                string result = startStr + " in " + endStr;
                result = result.Replace("\r", "");
                return result;
            }
            return fillTeace;
        }


    }
}
