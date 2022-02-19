using BepisModManager.Installation.Exceptions;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BepisModManager.Installation
{
    public class DllAnalyser
    {
        public Platform Platform
        {
            get;
            private set;
        }

        public string Headers
        {
            get;
            private set;
        }

        public DllAnalyser(string path) 
        {
            if (!path.EndsWith(".dll"))
                throw new BadFileException("File must be windows dynamic link library (.dll)");

            string location = Assembly.GetExecutingAssembly().Location;
            string executeDir = Path.GetDirectoryName(location);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(executeDir, @"dumpbin.exe"),
                    Arguments = $"\"{path}\" /headers",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                string output = process.StandardOutput.ReadToEnd();

                Headers = output;

                if (output.Contains("8664 machine (x64)"))
                    Platform = Platform.x64;
                else
                    Platform = Platform.x86;
            }
        }
    }
}
