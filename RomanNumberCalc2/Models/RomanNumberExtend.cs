using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumberCalc2.Models
{
    public class RomanNumberExtend : RomanNumber
    {
        private Dictionary<char, ushort> romanSymbols = new Dictionary<char, ushort>()
        {
            {'M', 1000},
            {'D', 500},
            {'C', 100},
            {'L', 50},
            {'X', 10},
            {'V', 5},
            {'I', 1}
         };
        public RomanNumberExtend(string num) : base(1)
        {
            ushort result = 0, firstNum, secondNum;

            for (int i = 0; i < num.Length; i++)
            {
                secondNum = 0;
                firstNum = romanSymbols[num[i]];
                if (i != num.Length - 1)
                {
                    secondNum = romanSymbols[num[i + 1]];
                }

                int ratio = secondNum / firstNum;
                if (ratio < 5 || ratio > 10)
                {
                    result += firstNum;
                    continue;
                }

                result += (ushort)(secondNum - firstNum);

                // пропуск следующего символа
                i++;
            }

            base.decimalNumber = result;
        }
    }
}
