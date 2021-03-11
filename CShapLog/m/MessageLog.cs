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

                string callPath = new StackTrace(4).ToString();  

                if (callPath != null)
                {
                    callPath = (callPath.Split(new string[] { "\n" }, StringSplitOptions.None)[0]).Replace("at", "");
                    //경로와 파라미터+시스템 경로 분리 
                    //ServerDaTransForm.Default.Da.LoginDa.SaveDa (System.String _token, System.Boolean _autoLoginFlag) [0x001fe] in <4526274351d54b789f4e183038001336>:0 
                    string[] pathList = (callPath.Split(new string[] { " (" }, StringSplitOptions.None));

                    //호출한 클래스와 메소드 명 추출
                    //LoginDa.SaveDa
                    string[] callFilePathList = pathList[0].Split('.');
                    string callFilePath = $"{callFilePathList[callFilePathList.Length - 2]}.{callFilePathList[callFilePathList.Length - 1]}";
                    //메소드 파라미터 추출
                    //(System.String _token, System.Boolean _autoLoginFlag)
                    string callArgs = $"({pathList[1].Split(')')[0]})";

                    callPath = callFilePath + callArgs;

                    callPath = callPath.Trim();
                    result.Append("  ");
                    result.Append(callPath);
                }
                result.Append("\n > [ Log ]  : ");

                foreach (object msssage in messages)
                {
                    result.Append(msssage);
                    result.Append("  ");
                }
                result.Append("\n\n");
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
     
    }
}
