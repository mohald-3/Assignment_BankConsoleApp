
namespace Bank_Console_App
{
	public class PersonalAccount : BankAccount
	{
		public PersonalAccount(string FirstName, string LastName, int InitialBalance)
			: base(FirstName, LastName, "Personal Account", InitialBalance)
		{ 
		}
	}
}
