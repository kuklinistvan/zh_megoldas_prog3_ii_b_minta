using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_minta_b
{
    public class TracertResultParser
    {
        private string output;

        public TracertResultParser(string output)
        {
            this.output = output;
        }

        public int NumberOfHops { 
            get
            {
                return int.Parse(output.Split('\n').Last(l => l.StartsWith("  ")).Split(' ')[2]);
            }
        }

        public int NumberOfTimeouts
        {
            get
            {
                return CountTimeoutsInTracerOutput(this.output);
            }
        }

        /// <summary>
        /// FIGYELEM! A futtatása növeli az alltimeouts számlálót.
        /// </summary>
        public static int CountTimeoutsInTracerOutput(string tracerOutput)
        {
            int numberOfTimeouts = tracerOutput.Split('\n').Count(line => line.Contains("Request timed out."));
            AsyncTracertTimeoutsCounter.IncreaseCounterBy(numberOfTimeouts);
            return numberOfTimeouts;
        }
    }
}
