using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public static class ShellManager
    {
        #region 第三层封装
        public static void AsyncImmediateRunCmd(string cmdLine, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            RunCmd(null, new string[] { cmdLine }, true, true, null, dataReceived, errorReceived);
        }
        public static void Execute(string filename,string aruments=null,bool isAdmin=false)
        {
            ShellExecuteCmd(isAdmin ? "RunAs" : "", filename, aruments,null,false,false,null,null,null);
        }
        #endregion
        #region 第二层封装
        public static string RunCmd(string arguments, string[] cmdLines, bool isWaitForExit, bool isAysnc, EventHandler exitHander, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            return ExecuteCmd("cmd.exe", arguments, cmdLines, isWaitForExit, isAysnc, exitHander, dataReceived, errorReceived);
        }
        public static string AdminRunCmd(string arguments, string[] cmdLines, bool isWaitForExit, bool isAysnc, EventHandler exitHander, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            return AdminExecuteCmd("cmd.exe", arguments, cmdLines, isWaitForExit, isAysnc, exitHander, dataReceived, errorReceived);
        }
        public static string AdminExecuteCmd(string fileName, string arguments, string[] cmdLines, bool isWaitForExit, bool isAysnc, EventHandler exitHander, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            return ShellExecuteCmd("RunAs", fileName, arguments, cmdLines, isWaitForExit, isAysnc, exitHander, dataReceived, errorReceived);
        }
        public static string ExecuteCmd(string fileName, string arguments, string[] cmdLines, bool isWaitForExit, bool isAysnc, EventHandler exitHander, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            return ShellExecuteCmd("", fileName, arguments, cmdLines, isWaitForExit, isAysnc, exitHander, dataReceived, errorReceived);
        }
        #endregion
        private static string ShellExecuteCmd(string verb, string fileName, string arguments, string[] cmdLines, bool isWaitForExit, bool isAysnc, EventHandler exitHander, System.Diagnostics.DataReceivedEventHandler dataReceived, System.Diagnostics.DataReceivedEventHandler errorReceived)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = fileName;
            if (!string.IsNullOrEmpty(arguments))
                startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            if (!string.IsNullOrEmpty(verb))
                startInfo.Verb = verb;

            string text = null;
            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo = startInfo;
                if (isAysnc)
                {
                    if (dataReceived != null)
                        proc.OutputDataReceived += dataReceived;
                    if (errorReceived != null)
                        proc.ErrorDataReceived += errorReceived;
                }
                proc.EnableRaisingEvents = true;
                proc.Exited += exitHander;
                proc.Start();
                if (cmdLines != null)
                {
                    using (System.IO.StreamWriter writer = proc.StandardInput)
                    {
                        foreach (var line in cmdLines)
                        {
                            proc.StandardInput.WriteLine(line);
                        }
                    }
                    if (isAysnc)
                    {
                        proc.BeginOutputReadLine();
                        proc.BeginErrorReadLine();
                    }
                    else
                        text = proc.StandardOutput.ReadToEnd();
                }

                if (isWaitForExit)
                    proc.WaitForExit();
            }
            return text;
        }
    }
}
