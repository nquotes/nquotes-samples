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
            
            if (total ==0 || isMarried)
                return "You're broke!"; 

            Random random = new Random(int.Parse(birthday.ToString("MMddHHmmss")));
            int prediction = random.Next(0, total);
            string answer;
            if (total < 100)
            {
                answer = $"You're not rich but also not broke. That's a good thing. You're worth {prediction} today!";
            }
            else
            {
                answer = $"You're rich! Well done! You're worth {prediction} today!";
            }
            
            total = 0;
            return answer;
        }
    }
}
