using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLLMethodCaller
{
    class FortuneTeller
    {
        private int total;
        private int fortuneTellerFee = 1;
        public void AcceptMoney(int amount)
        {
            total += amount;
        }

        public string PredictFuture(DateTime birthday, bool isMarried)
        {
            total -= fortuneTellerFee;
            if (total ==0 || isMarried)
                return "You're broke!";
            if (total < 0 )
                return "You're in debt!";

            Random random = new Random(int.Parse(birthday.ToString("MMddHHmmss")));
            return $"You're {(total<100?"not so":"")} rich. You're worth {random.Next(0, total)} today!";
        }
    }
}
