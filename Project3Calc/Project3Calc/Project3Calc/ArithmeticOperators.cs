using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Project3Calc
{
    class ArithmeticOperators
    {
        private int x;
        private int y;

        public ArithmeticOperators(int num1, int num2)
        {
            x = num1;
            y = num2;
        }

        public int Solve(Button buttonClicked)
        {
            if (buttonClicked.Text.Contains("+"))
            {
                return x + y;
            }
            else if (buttonClicked.Text.Contains("-"))
            {
                return x - y;
            }
            else if (buttonClicked.Text.Contains("X"))
            {
                return x * y;
            }
            else
            {
                return Convert.ToInt32(x / y);
            }
        }

    }
}
