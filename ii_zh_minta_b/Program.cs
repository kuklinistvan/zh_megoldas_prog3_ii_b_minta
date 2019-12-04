using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_minta_b
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] domains = AskForDomains();
            RunTracerts(domains);
        }

        private static string[] AskForDomains()
        {
            LinkedList<string> domains = new LinkedList<string>();
            
            for(; ; )
            {
                Console.Write("Domain (vagy üres, ha kész) > ");
                string resultBuffer = Console.ReadLine().Trim();
                if (resultBuffer != string.Empty)
                {
                    domains.AddLast(resultBuffer);
                }
                else
                {
                    break;
                }
            }

            return domains.ToArray();
        }

        public static void RunTracerts(string[] targets)
        {
            LinkedList<Task> tasksPending = new LinkedList<Task>();

            TracertRunner[] runners = targets.Select(t => new TracertRunner(t)).ToArray();

            foreach (TracertRunner runner in runners)
            {
                Task task = new Task(() => runner.RunTracert());
                task.ContinueWith(t => WriteResultToConsoleOf(runner));
                tasksPending.AddLast(task);
            }

            Task.WhenAll(tasksPending).ContinueWith(t =>
            {
                Console.WriteLine($"Összesen {AsyncTracertTimeoutsCounter.Counter} timeout-ot tapasztaltunk.");
            });

            tasksPending.ToList().ForEach(t => t.Start());

            Console.WriteLine("ENTER lenyomásával bármikor kiléphet.");
            Console.ReadLine();
        }

        private static void WriteResultToConsoleOf(TracertRunner runner)
        {
            string domain = runner.Target;
            TracertResultParser results = runner.ResultParser;
            Console.WriteLine($"{domain}: {results.NumberOfHops} hops, {results.NumberOfTimeouts} timeouts");
            // google.com: 8 hops, 2 timeouts
        }
    }
}
