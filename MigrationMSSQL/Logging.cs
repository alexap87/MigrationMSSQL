using System;
using System.Diagnostics;
using System.IO;

namespace MigrationMSSQL
{
    class Logging
    {
        public void writeLog(string message)
        {
            string pathDir = @"c:\\MigrationMSSQL";
            DirectoryInfo dirInfo = new DirectoryInfo(pathDir);
            if (!dirInfo.Exists)
            {
                try
                {
                    dirInfo = new DirectoryInfo(pathDir);
                    dirInfo.Create();
                    writeOrCreatetxt(message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                writeOrCreatetxt(message);
            }
        }
        void writeOrCreatetxt(string message)
        {
            string path = @"c:\\MigrationMSSQL\log_migration";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("File create: {0}", DateTime.Now.ToString());
                    sw.WriteLine("Starting the service for the first time");
                }
            }
            else
            {
                string messages = String.Format("\n{0}: {1} ", DateTime.Now.ToString(), message);
                File.AppendAllText(path, messages);
            }
        }

    }
}
