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
        //[System.Diagnostics.Conditional("DEBUG")]
        public static void e(Exception e, string Token = null)
        {
            try
            {
                //DetailExceptionLog(e); // select 1
                SimpleExceptionLog(e);// select 2
                if (Token != null) SendLogSave(result, Token);
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

   
        private static string DetailExceptionLog(Exception e)
        {
            try
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
                result.Append("[   Type    ]  : ");
                result.Append(e.GetType());

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
                return result.ToString();

            }
            catch (Exception LogE)
            {
                return LogE.Message;
            }




        }
        private static string SimpleExceptionLog(Exception e)
        {
            try
            {
                StackTrace stackTrace = new StackTrace(true);
                StringBuilder result = new StringBuilder();
                int count = Convert.ToInt32(stackTrace.FrameCount);

                count = count > 3 ? 3 : count;

                string today = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                result.Append("  ---- StackTrace Start ----   ");
                result.Append("\\r\\n");
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

                System.Diagnostics.Debug.WriteLine(result);


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
                int classLocation = fullFile.LastIndexOf("\\");
                string FileName = fullFile.Substring(classLocation + 1);
                result.Append(FileName);
            }
            else
            {
                result.Append("NULL");
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

        private static async Task<bool> SendLogSave(string log, string token)
        {
            log = log.Replace("\r\n", "\\r\\n"); //서버에게 보내는 log 는 개행문자가 다르다.
            string uri = "server URI";

            string sendData = "{" + "\"log\":\"" + log + "\"}";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Accept = "*/*";
            req.Method = "POST";
            req.ContentType = "application/json;charset=UTF-8";
            req.KeepAlive = true;
            req.ServerCertificateValidationCallback = delegate { return true; };
            req.Timeout = 4000;
            req.Headers.Add("X-AUTH-TOKEN", token);
            try
            {

                StreamWriter writer = new StreamWriter(await req.GetRequestStreamAsync());
                writer.Write(sendData);
                writer.Close();

                HttpWebResponse result = (HttpWebResponse)await req.GetResponseAsync();

                Encoding encode = Encoding.GetEncoding("utf-8");
                Stream strReceiveStream = result.GetResponseStream();
                StreamReader reqStreamReader = new StreamReader(strReceiveStream, encode);
                string strResult = reqStreamReader.ReadToEnd();
                result.Close();
                reqStreamReader.Close();
                strReceiveStream.Close();
                return true;

            }
            catch (WebException we)
            {
                DebugLog.m("[WebException] : " + we.Message);

                return false;
            }
            catch (Exception LogE)
            {
                DebugLog.m(LogE.Message);
                return false;
            }
            finally
            {
                req.Abort();
            }
        }
    }


}
