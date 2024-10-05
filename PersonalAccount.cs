
namespace Bank_Console_App
{
	public class PersonalAccount : BankAccount
	{
		public PersonalAccount(string FirstName, string LastName, int AccountNumber, int InitialBalance)
			: base(FirstName, LastName, AccountNumber, "Personal Account", InitialBalance)
		{ 
		}
	}
}
// public BankAccount(string FirstName, string LastName, int AccountNumber, string Accounttype, int InitialBalance)
