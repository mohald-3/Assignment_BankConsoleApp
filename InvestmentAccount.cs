
namespace Bank_Console_App
{
    public class InvestmentAccount : BankAccount
    {
        public InvestmentAccount(string FirstName, string LastName, int InitialBalance)
            : base(FirstName, LastName, "Investment Account", InitialBalance)
        {
        }
    }
}
