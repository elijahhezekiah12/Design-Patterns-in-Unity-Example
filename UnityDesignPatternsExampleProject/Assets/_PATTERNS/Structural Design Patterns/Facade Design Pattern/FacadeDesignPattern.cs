﻿using UnityEngine;
using System.Collections;

/* Use when you create a simplified interface that performs many other actions behind the scenes
 * 
 * e.g. Can I withdraw $50 from the bank
 * 	-> check if account nr is corrent
 * 	-> check if security code if valid
 * 	-> check if funds are available
 * 	-> make changes accordingly
 * 
 * */


namespace FacadePattern
{
	public class FacadeDesignPattern : MonoBehaviour
	{
		void OnEnable ()
		{
			Debug.Log ("------------------");
			Debug.Log ("FACADE DESIGN PATTERN");

			BankAccountFacade bankAccount = new BankAccountFacade (12345678, 1234);

			Debug.Log ("\"I want to withdraw $50 dollars!\"");
			bankAccount.WithdrawCash (50.00);

			Debug.Log ("\"Mh ok now let me withdraw $5000 dollars!?\"");
			bankAccount.WithdrawCash (5000.00);

			Debug.Log ("\"What tf... maybe save some cash, here are $300 bucks ;-)\"");
			bankAccount.DepositCash (300.00);
		}
	}

	public class WelcomeToBank
	{
		public WelcomeToBank ()
		{
			Debug.Log ("Welcome to ABC Bank");
			Debug.Log ("We are happy to deposit your money :-)");
		}
	}

	public class AccountNumberCheck
	{
		private int accountNumber = 12345678;

		public int GetAccountNumber ()
		{
			return accountNumber;
		}

		public bool AccountActive (int accNumber)
		{
			if (accNumber == accountNumber)
				return true;
			else
				return false;
		}
	}

	public class SecurityCodeCheck
	{
		private int securityCode = 1234;
		
		public int GetSecurityCode ()
		{
			return securityCode;
		}
		
		public bool IsCodeCorrect (int code)
		{
			if (code == securityCode)
				return true;
			else
				return false;
		}
	}

	public class FundsCheck
	{
		private double cashInAccount = 1000.00;

		public double GetCashInAccount ()
		{
			return cashInAccount;
		}

		protected void DecreaseCashInAccount (double cash)
		{
			cashInAccount -= cash;
		}
		
		protected void IncreaseCashInAccount (double cash)
		{
			cashInAccount += cash;
		}

		public bool HaveEnoughMoney (double cashToWithdraw)
		{
			if (cashToWithdraw > GetCashInAccount ())
			{
				Debug.Log ("You don't have enouth money.");
				return false;
			}
			else
			{
				return true;
			}
		}

		public void MakeDeposit (double cash)
		{
			IncreaseCashInAccount (cash);
			Debug.Log ("Deposit complete. Current Balance is: " + GetCashInAccount ());
		}
		
		public void WithdrawMoney (double cash)
		{
			DecreaseCashInAccount (cash);
			Debug.Log ("Withdraw complete. Current Balance is: " + GetCashInAccount ());
		}
	}

	public class BankAccountFacade
	{
		private int accountNumber;
		private int securityCode;
		AccountNumberCheck accChecker;
		SecurityCodeCheck codeChecker;
		FundsCheck fundChecker;
		WelcomeToBank bankWelcome;

		public BankAccountFacade (int accountNumber, int newSecurityCode)
		{
			this.accountNumber = accountNumber;
			this.securityCode = newSecurityCode;

			bankWelcome = new WelcomeToBank ();
			codeChecker = new SecurityCodeCheck ();
			accChecker = new AccountNumberCheck ();
			fundChecker = new FundsCheck ();
		}

		public int GetAccountNumber ()
		{
			return accountNumber;
		}

		public int GetSecurityCode ()
		{
			return securityCode;
		}

		public void WithdrawCash (double cash)
		{
			if (accChecker.AccountActive (GetAccountNumber ())
				&& codeChecker.IsCodeCorrect (GetSecurityCode ())
				&& fundChecker.HaveEnoughMoney (cash))
			{
				fundChecker.WithdrawMoney(cash);
				Debug.Log ("Transaction complete.");
			}
			else
			{
				Debug.Log ("Transaction failed.");
			}
		}

		public void DepositCash(double cash)
		{
			if (accChecker.AccountActive (GetAccountNumber ())
			    && codeChecker.IsCodeCorrect (GetSecurityCode ()))
			{
				fundChecker.MakeDeposit(cash);
				Debug.Log ("Transaction complete.");
			}
			else
			{
				Debug.Log ("Transaction failed.");
			}
		}
	}

}