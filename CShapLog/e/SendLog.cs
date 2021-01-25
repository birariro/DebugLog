using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CShapLog.e
{
    public class SendLog
    {
        static private SendLog instance;
        private SendLog()
        { }

        public static async void Call(string token , string log)
        {
            if (instance == null) instance = new SendLog();
            await instance.Send(token,log);
        }
        private async Task<bool> Send(string token, string log)
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
