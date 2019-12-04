using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_minta_b
{
    class TracertRunner
    {
        private string target;
        private string result;
        private TracertResultParser resultParser;

        public TracertRunner(string target)
        {
            this.target = target;
        }
        public void RunTracert()
        {
            Console.WriteLine("Tracert indítása a " + target + " domain-re.");
            this.result = RunTracertCmd(target);
            this.resultParser = new TracertResultParser(result);
        }

        public TracertResultParser ResultParser { get => this.resultParser; }

        public string Target { get => target; }

        private static string RunTracertCmd(string target)
        {
            Process proc = Process.Start(new ProcessStartInfo("tracert", target)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
            });
            proc.WaitForExit();
            return proc.StandardOutput.ReadToEnd();
        }
    }
}
