/*
 * Created by SharpDevelop.
 * Date: 31.08.2017
 * Time: 20:32
 *
 */

using System;
using System.IO;
using System.Reflection;

namespace Log4CSharp
{
    /// <summary>
    /// Description of LogWriter.
    /// </summary>
    public static class LogWriter
    {
        /// <summary>
        /// Name of the Logfile to be created, defaults to log.txt
        /// </summary>
        public static string LogFileName
        {
            get => _logFileName;
            set => _logFileName = value;
        }
        private static string _logFileName = "log.txt";

        private static StreamWriter textStream;
        private static readonly string FacilityName = Assembly.GetEntryAssembly().GetName().Name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public static void CreateLogEntry(string e)
        {
            var _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FacilityName, "log");

            if (!Directory.Exists(_logFilePath))
            {
                Directory.CreateDirectory(_logFilePath);
            }

            try
            {
                if (!File.Exists(Path.Combine(_logFilePath, _logFileName)))
                {
                    textStream = File.CreateText(Path.Combine(_logFilePath, _logFileName));
                }
                else
                {
                    textStream = File.AppendText(Path.Combine(_logFilePath, _logFileName));
                }

                textStream.WriteAsync(
                    string.Format("{0}" + Environment.NewLine,
                    string.Format("{0}: {1}; {2}", DateTime.Now, e).Replace("\r\n", "; ")));
                textStream.Close();
                textStream.Dispose();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public static void CreateLogEntry(Exception e)
        {
            var _logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FacilityName, "log");

            if (!Directory.Exists(_logFilePath))
            {
                Directory.CreateDirectory(_logFilePath);
            }

            try
            {
                if (!File.Exists(Path.Combine(_logFilePath, _logFileName)))
                {
                    textStream = File.CreateText(Path.Combine(_logFilePath, _logFileName));
                }
                else
                {
                    textStream = File.AppendText(Path.Combine(_logFilePath, _logFileName));
                }

                textStream.WriteAsync(
                    string.Format("{0}" + Environment.NewLine,
                    string.Format("{0}: {1}; {2}", DateTime.Now, e.Message, e.InnerException != null ? e.InnerException.Message : "").Replace("\r\n", "; ")));
                textStream.Close();
                textStream.Dispose();
            }
            catch
            {
            }
        }
    }
}