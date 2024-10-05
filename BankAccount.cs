
namespace Bank_Console_App
{
    public abstract class BankAccount
    {
        string? accountHolderFirstName {  get; set; }
        string? accountHolderLastName { get; set; }
        public int accountNumber { get; set; }
        public string? accountType { get; set; }
        public int balance { get; set; }

        // creates a list of tuples
        List<(DateTime time, int amount, int fromAccount, int toAccount )> TransferEvents = new List<(DateTime, int, int, int)>(); 

        public BankAccount(string FirstName, string LastName, int AccountNumber, string Accounttype, int InitialBalance)
        {
            accountHolderFirstName = FirstName;
            accountHolderLastName = LastName;
            accountNumber = AccountNumber;
            accountType = Accounttype;
            balance = InitialBalance;
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your current balance is {balance:SEK}");
            
        }

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"{amount:SEK} deposited. New balance: {balance:SEK}");
        }

        public void Withdraw(int amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"{amount:SEK} withdrawn. New balance: {balance:SEK}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        public void TransferMoney()
        {

        }


    }
}

//using System.Collections.Generic; // NOT FINISHED
//private HashSet<int> accountIDs = new HashSet<int>();  // Keeps track of used accountIDs NOT FINISHED
//private Random random = new Random();  // Random number generator NOT FINISHED