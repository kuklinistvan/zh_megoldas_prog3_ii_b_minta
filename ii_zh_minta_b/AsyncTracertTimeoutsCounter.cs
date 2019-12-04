using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_minta_b
{
    public class AsyncTracertTimeoutsCounter
    {
        private static object timeoutLock = new object();
        private static int alltimeouts = 0;

        public static void IncreaseCounterBy(int amount)
        {
            lock(timeoutLock)
            {
                alltimeouts += amount;
            }
        }

        public static int Counter { get => alltimeouts; }
    }
}
