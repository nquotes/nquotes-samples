using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLMethodCaller
{
    class FortuneTeller
    {
        private int total;
        public void AcceptMoney(int amount)
        {
            total += amount;
        }

        public string PredictFuture(DateTime birthday, bool isMarried)
        {
            if (isMarried)
                return "You're broke!";

            Random random = new Random(int.Parse(birthday.ToString("yyyyMMdd")));
            lock (random) // synchronize
            {
                return $"You're rich, You're worth {random.Next(0, total)} today!";
            }
            return "";
        }
    }
}
