using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Project3Calc;
using System.Collections.ObjectModel;

namespace Project3Calc
{
    public partial class MainPage : ContentPage
    {
        // Declared to later store the previous number entered
        private int previousNum;
        // Declared to later store the last operation button clicked, such as +, -, M-
        private Button buttonClicked;
        // Will contain the value held in the memory registrey
        private int memoryRegistry = 0;

        // Used to store values
        InvestmentHandler investmentHandler = new InvestmentHandler();

        /**
        * ClearClicked responds to when the clear button is pressed
        * 
        * @param sender
        * @param args
        */
        public void ClearClicked(object sender, EventArgs args)
        {
            numberLabel.Text = "0";
            CheckSign();
        }

        /**
        * InverOperationClicked responds to when the +/- is clicked and switches sign
        * 
        * @param sender
        * @param args
        */
        public void InverseOperatorClicked(object sender, EventArgs args)
        {
            numberLabel.Text = (Int32.Parse(numberLabel.Text) * -1).ToString();
            CheckSign();
        }

        /**
        * NumberClicked responds to when a number button is pressed and responds
        * with the correct number
        * 
        * @param sender
        * @param args
        */
        public void NumberClicked(object sender, EventArgs args)
        {
            try
            {
                Int32.Parse(numberLabel.Text);
            } catch
            {
                numberLabel.Text = "0";
            }
            // Create a list of our buttons to go through to see if one of the was pressed
            Button[] numberOptions = { zero, one, two, three, four, five, six, seven, eight, nine };
            // Loop through our button options
            foreach (Button number in numberOptions)
            {
                if (sender == number)
                {
                    if (numberLabel.Text == "0") numberLabel.Text = number.Text;
                    else if (sender != zero || numberLabel.Text.Length > 0) numberLabel.Text = numberLabel.Text + number.Text;
                }
            }
            CheckSign();
        }

        /**
        * NumberClicked responds to when a number button is pressed and responds
        * with the correct number
        * 
        * @param sender
        * @param args
        */
        public void MemoryRegisterClicked(object sender, EventArgs args)
        {
            // Add/Subtract the current value to/from the stored value in the memory register
            if (sender == mAdd || sender == mSub)
            {
                ArithmeticOperators ao = new ArithmeticOperators(memoryRegistry, Int32.Parse(numberLabel.Text));
                int output = ao.Solve((Button)sender);
                memoryRegistry = output;
                numberLabel.Text = output.ToString();
            }

            // Recall the current memory register value
            else if (sender == mr)
            {
                numberLabel.Text = memoryRegistry.ToString();
            }
            // Clear the memory register
            else if (sender == mc)
            {
                memoryRegistry = 0;
                numberLabel.Text = "MR CLEARED";
            }
            CheckSign();
        }

        /**
        * ArithmeticOperatorClicked handles the events for any of the arithmetic
        * operators being pressed
        * 
        * @param sender
        * @param args
        */
        public void ArithmeticOperatorClicked(object sender, EventArgs args)
        {
            buttonClicked = (Button)sender;
            previousNum = Int32.Parse(numberLabel.Text);
            numberLabel.Text = "0";
            CheckSign();
        }

        /**
        * InvestmentButtonsClicked handles the events for any of the investments
        * operators being pressed like RATE, INVEST, FREQ
        * 
        * @param sender
        * @param args
        */
        public void InvestmentButtonsClicked(object sender, EventArgs args)
        {
            Button buttonClicked = (Button)sender;
            if (buttonClicked.Text == "FREQ")
            {
                pickerFreq.IsVisible = !pickerFreq.IsVisible;

                investmentHandler.freq = (string)pickerFreq.SelectedItem;
                labelFreq.Text = "Frequency of investment: " + (string)pickerFreq.SelectedItem;
            }
            if (buttonClicked.Text == "INVEST")
            {
                investmentHandler.regularInvestment = Int32.Parse(numberLabel.Text);
                labelInvest.Text = "Regular Investment: $" + numberLabel.Text;
            }
            if (buttonClicked.Text == "RATE")
            {
                double asDecimal = Double.Parse(numberLabel.Text) / 100;
                investmentHandler.rateOfReturn = asDecimal;
                labelRate.Text = "Rate of return: " + numberLabel.Text + "%";
            }
            if (buttonClicked.Text == "YEARS")
            {
                investmentHandler.yrs = Int32.Parse(numberLabel.Text);
                labelYears.Text = "Years: " + numberLabel.Text;
            }
            if (buttonClicked.Text == "START")
            {
                investmentHandler.startingAmount = Int32.Parse(numberLabel.Text);
                labelStartingAmount.Text = "Starting amount: " + numberLabel.Text;
            }
            numberLabel.Text = "0";
        }

        /**
         * EqualsSignClicked handles the event where the "=" is clicked. It then calls the
         * ArithmeticOperators Solve function to return the answer
         * 
         * @param sender
         * @param args
         */
        public void EqualSignClicked (object sender, EventArgs args)
        {
            ArithmeticOperators ao = new ArithmeticOperators(previousNum, Int32.Parse(numberLabel.Text));
            numberLabel.Text = ao.Solve(buttonClicked).ToString();
            CheckSign();
        }

        /**
         * FinalClicked handles the event where the "final" is clicked. It then calls the
         * investmentHandler Compute function to return the answer
         * 
         * @param sender
         * @param args
         */
        public void FinalClicked(object sender, EventArgs args)
        {
            labelFinalBalance.Text = "Final Balance: $" + investmentHandler.Compute().ToString();
        }

        public void CheckSign()
        {
            if (numberLabel.Text.Contains("-"))
            {
                buttonInvest.IsEnabled = false;
                buttonRate.IsEnabled = false;
                buttonStart.IsEnabled = false;
                buttonYears.IsEnabled = false;
            }
            else
            {
                buttonInvest.IsEnabled = true;
                buttonRate.IsEnabled = true;
                buttonStart.IsEnabled = true;
                buttonYears.IsEnabled = true;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
