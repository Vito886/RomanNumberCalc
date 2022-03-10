using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using RomanNumberCalc2.Models;

namespace RomanNumberCalc2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string output = "";
        RomanNumberExtend firstNumber;
        string operatorSymbol = "";
        public MainWindowViewModel()
        {
            OnClickCommand = ReactiveCommand.Create<string, string>((str) => Output = str);
        }
        public string Output
        {
            set
            {
                if (output == "Ошибка")
                {
                    output = "";
                    operatorSymbol = "";
                }
                try
                {
                    if (value == "+" || value == "-" || value == "*" || value == "/")
                    {
                        if (operatorSymbol != "")
                        {
                            throw new RomanNumberException("Введено больше 1 оператора");
                        }
                        if (output == "")
                        {
                            throw new RomanNumberException("Нельзя выполнить действие до ввода первого числа");
                        }
                        firstNumber = new RomanNumberExtend(output);
                        operatorSymbol = value;
                        this.RaiseAndSetIfChanged(ref output, value);
                        return;
                    }
                    if (value == "=")
                    {
                        if (operatorSymbol == "")
                        {
                            throw new RomanNumberException("Не введён оператор");
                        }
                        if (output == "")
                        {
                            throw new RomanNumberException("Нельзя выполнить действие до ввода второго числа");
                        }
                        RomanNumberExtend a = new RomanNumberExtend(output);
                        if (operatorSymbol == "+")
                        {
                            RomanNumber result = firstNumber + a;
                            this.RaiseAndSetIfChanged(ref output, result.ToString());
                        }
                        if (operatorSymbol == "-")
                        {
                            RomanNumber result = firstNumber - a;
                            this.RaiseAndSetIfChanged(ref output, result.ToString());
                        }
                        if (operatorSymbol == "*")
                        {
                            RomanNumber result = firstNumber * a;
                            this.RaiseAndSetIfChanged(ref output, result.ToString());
                        }
                        if (operatorSymbol == "/")
                        {
                            RomanNumber result = firstNumber / a;
                            this.RaiseAndSetIfChanged(ref output, result.ToString());
                        }
                        operatorSymbol = "";
                        return;
                    }
                    if (output == "+" || output == "-" || output == "*" || output == "/")
                    {
                        this.RaiseAndSetIfChanged(ref output, value);
                        return;
                    }
                    this.RaiseAndSetIfChanged(ref output, output + value);
                }
                catch
                {
                    this.RaiseAndSetIfChanged(ref output, "Ошибка");
                }
            }
            get
            {
                return output;
            }
        }
        public ReactiveCommand<string, string> OnClickCommand { get; set; }
    }
}
