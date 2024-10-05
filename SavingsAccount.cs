
namespace Bank_Console_App
{
    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(string FirstName, string LastName, int AccountNumber, int InitialBalance)
            : base(FirstName, LastName, AccountNumber, "Savings Account", InitialBalance)
        {
        }
    }
}