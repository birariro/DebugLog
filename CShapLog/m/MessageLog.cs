using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShapLog.m
{
    public class MessageLog
    {
        static private MessageLog instance;
        private MessageLog()
        { }

        public static void Call(params object[] messages)
        {
            if (instance == null) instance = new MessageLog();
            instance.Print(messages);
        }

        private void Print(params object[] messages)
        {
            try
            {
                string today = DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss");
                StringBuilder result = new StringBuilder();
                result.Append("-> ");
                result.Append(today);

                string callPath = new StackTrace(2).ToString();  //로그를 호출한 위치

                if (callPath != null)
                {
                    callPath = (callPath.Split(new string[] { "\n" }, StringSplitOptions.None)[0]).Replace("at", "");
                    callPath = callPath.Trim();
                    result.Append("/");
                    result.Append(callPath);
                }
                result.Append("\n > [ Log ]  : ");

                foreach (object msssage in messages)
                {
                    result.Append(msssage);
                    result.Append("  ");
                }
                result.Append("\n");
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
