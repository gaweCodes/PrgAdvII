using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPNCalculator {
    class Program {
        static void Main(string[] args) {
            // RPN = Reverse Polnish Notation (bekannt von den HP-Taschenrechnern)

            var calc = new RPNCalculatorSvrLib.RPNCalculator();
            calc.StackEmpty += Calc_StackEmpty;
            calc.Push(3);
            calc.Push(4);
            calc.Mul();
            Console.WriteLine(calc.Pop());

            calc.Add();
        }

        private static void Calc_StackEmpty() {
            Console.WriteLine("Stack empty!");
        }
    }
}
