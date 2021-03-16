using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Project3Calc
{
    class InvestmentHandler
    {
        public int startingAmount = 0;
        public int yrs = 30;
        public double rateOfReturn = 0.05;
        public int regularInvestment = 500;
        public string freq = "Monthly";
        public int finalBalance = 0;

		/*
		 * start is the starting balance
		 * years is the number of years that the investment will encompass
		 * perc is the annual rate of return (e.g., 6% should sent in as 0.06)
		 * investment is the amount of money added to the account on a regular basis
		 * depositsPerYear is the number times per year (12, 4, or 1) that a deposit is made.
		*/
		public int Compute()
		{
			// Convert freq into a number assigned to depositsPerYear
			int depositsPerYear;
			if (freq == "Monthly") depositsPerYear = 12;
			else if (freq == "Quarterly") depositsPerYear = 4;
			else depositsPerYear = 1;

			double bal = startingAmount;
			double monthlyRate = rateOfReturn / 12.0;
			int monthsToDeposit = 12 / depositsPerYear;
			for (int y = 0; y < yrs; y++)
			{
				for (int m = 1; m <= 12; m++)
				{
					bal += bal * monthlyRate;
					if (m % monthsToDeposit == 0)
					{
						bal += regularInvestment;      // make deposits at the end of the month
					}
				}
			}
			return (int)Math.Round(bal);
		}

	}
}
