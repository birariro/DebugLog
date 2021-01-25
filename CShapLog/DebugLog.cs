using CShapLog.e;
using CShapLog.m;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShapLog
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
            MessageLog.Call(messages);
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
            string log = ExceptionLog.Call(e);
            Console.WriteLine(log);
        }

        /// <summary>
        /// debug mode shows logcat and send to server.
        /// Logcat not displayed in release mode
        /// But it's sent to the server.
        /// Add exceptions as parameters.
        /// Then add the required variables to the server.
        /// </summary>
        /// <param name="e">data type Exception</param>
        /// <param name="token">variables to the server.</param>
        //[System.Diagnostics.Conditional("DEBUG")]
        public static void e(Exception e, string token)
        {
            string log = ExceptionLog.Call(e);
            Console.WriteLine(log);
            SendLog.Call(token, log);

        }


    }
}
