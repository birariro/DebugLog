using CShapLog.e;
using CShapLog.m;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShapLog
{
    /// <summary>
    /// C# 에서 사용될 Logger 이다.
    /// Logger.m() 을통해 Console에 원하는 문자열을 출력할수있다.
    /// Logger.e() 를통해 Console에  Exception 내용을 출력할수있다.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Debug 에서는 Console 에 출력된다.
        /// Release 에서는 동작하지 않는다.
        /// </summary>
        /// <param name="messages">아무값이나 넣어 출력할수있다.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void m(params object[] messages)
        {
            MessageLog.Call(messages);
        }



        /// <summary>
        /// debug 에서는 Console에 출력하고 서버에게 전달할수있다.
        /// Release 에서는 Console에 출력하지않고 서버에게 전달한다.
        /// 만약 서버에게 전달하지않을거면 SendLog.Call() 을 제거해라
        /// </summary>
        /// <param name="exception">Exception 을 넣는다.</param>
        /// <param name="token">서버에게 필요한 데이터</param>
        public static void e(Exception exception, string token="")
        {
            string log = ExceptionLog.Call(exception);
            DebugPrint(log);
            SendLog.Call(token, log);

        }

        [System.Diagnostics.Conditional("DEBUG")]
        private static void DebugPrint(string log)
        {
            Console.WriteLine(log); 
        }
        

       


    }
}
