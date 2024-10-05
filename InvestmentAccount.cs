
namespace Bank_Console_App
{
    public class InvestmentAccount : BankAccount
    {
        public InvestmentAccount(string FirstName, string LastName, int AccountNumber, int InitialBalance)
            : base(FirstName, LastName, AccountNumber, "Investment Account", InitialBalance)
        {
        }
    }


}
