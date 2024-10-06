
namespace Bank_Console_App
{
    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(string FirstName, string LastName, int InitialBalance)
            : base(FirstName, LastName, "Savings Account", InitialBalance)
        {
        }
    }
}