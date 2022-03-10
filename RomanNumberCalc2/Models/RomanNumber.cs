using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumberCalc2.Models
{
    public class RomanNumber : ICloneable, IComparable
    {
        protected string romanNumber = "";
        protected ushort decimalNumber;
        //Конструктор получает число n, которое должен представлять объект класса
        public RomanNumber(ushort n)
        {
            decimalNumber = n;
            if (romanNumber == "")
            {
                if (n <= 0 || n >= 4000)
                {
                    throw new RomanNumberException("Некорректное десятичное число");
                }
                string[] romanDigits = new string[] { "M", "D", "C", "L", "X", "V", "I" };
                ushort romanDigitsValue = 1000;
                for (int i = 0; i < romanDigits.Length; i += 2)
                {
                    int digit = n / romanDigitsValue;

                    if (digit == 9)
                    {
                        romanNumber += romanDigits[i] + romanDigits[i - 2];
                        digit = 0;
                    }
                    if (digit >= 5)
                    {
                        romanNumber += romanDigits[i - 1];
                        digit -= 5;
                    }
                    if (digit == 4)
                    {
                        romanNumber += romanDigits[i] + romanDigits[i - 1];
                        digit = 0;
                    }
                    for (int j = 0; j < digit; j++)
                    {
                        romanNumber += romanDigits[i];
                    }

                    n %= romanDigitsValue;
                    romanDigitsValue /= 10;
                }
            }
        }
        //Сложение римских чисел
        public static RomanNumber operator +(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new RomanNumberException("Некорректные входные данные");
            }
            return new RomanNumber((ushort)(n1.decimalNumber + n2.decimalNumber));
        }
        //Вычитание римских чисел
        public static RomanNumber operator -(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null || n1.decimalNumber - n2.decimalNumber <= 0)
            {
                throw new RomanNumberException("Некорректные входные данные");
            }

            return new RomanNumber((ushort)(n1.decimalNumber - n2.decimalNumber));
        }
        //Умножение римских чисел
        public static RomanNumber operator *(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new RomanNumberException("Некорректные входные данные");
            }
            return new RomanNumber((ushort)(n1.decimalNumber * n2.decimalNumber));
        }
        //Целочисленное деление римских чисел
        public static RomanNumber operator /(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null || n1.decimalNumber / n2.decimalNumber <= 0)
            {
                throw new RomanNumberException("Некорректные входные данные");
            }

            return new RomanNumber((ushort)(n1.decimalNumber / n2.decimalNumber));
        }
        //Возвращает строковое представление римского числа
        public override string ToString()
        {
            return romanNumber;
        }

        public object Clone()
        {
            return new RomanNumber(decimalNumber);
        }

        public int CompareTo(object? o)
        {
            if (o is RomanNumber number)
            {
                return number.decimalNumber - decimalNumber;
            }
            else throw new ArgumentException("Некорректное значение параметра");
        }
    }

}
