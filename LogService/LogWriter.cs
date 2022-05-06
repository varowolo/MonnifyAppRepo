using System;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace MonnifyApp.LogService
{
    public class LogWriter : ILogWriter
    {
        private readonly IHostEnvironment env;

        private string m_exePath = string.Empty;

        public LogWriter(IHostEnvironment env)
        {
            this.env = env;

        }


        public string LogWarn(string logMessage, string type)
        {
            try
            {
                Directory.CreateDirectory(env.ContentRootPath + "/Log/" + type.ToString() + "/");
                m_exePath = env.ContentRootPath + "/Log/" + type.ToString() + "/" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(m_exePath))
                {
                    using (StreamWriter sw = File.CreateText(m_exePath))
                    {
                        Log(logMessage, sw);
                    }
                }
                else
                {
                    using (StreamWriter w = File.AppendText(m_exePath))
                    {
                        Log(logMessage, w);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string LogWrite(string logMessage, string type)
        {
            try
            {
                Directory.CreateDirectory(env.ContentRootPath + "/Log/" + type.ToString() + "/");
                m_exePath = env.ContentRootPath + "/Log/" + type.ToString() + "/" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                if (!File.Exists(m_exePath))
                {
                    using (StreamWriter sw = File.CreateText(m_exePath))
                    {
                        Log(logMessage, sw);
                    }
                }
                else
                {
                    using (StreamWriter w = File.AppendText(m_exePath))
                    {
                        Log(logMessage, w);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0}", DateTime.Now.ToLongTimeString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
