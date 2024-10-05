

namespace Bank_Console_App
{
    public abstract class BankAccount
    {
        string? accountHolderName {  get; set; }
        public int accountNumber { get; set; }
        public string? accountType { get; set; }
        public int balance { get; set; }

        // creates a list of tuples
        List<(DateTime time, int amount, int fromAccount, int toAccount )> TransferEvents = new List<(DateTime, int, int, int)>(); 

        public BankAccount(string firstName, string lastName, int number, string type, int initialBalance)
        {
            accountHolderFirstName = firstName;
            accountHolderLastName = lastName;
            accountNumber = number;
            accountType = type;
            balance = initialBalance;
        }

        public int CheckBalance()
        {
            return balance;
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


    }
}

//using System.Collections.Generic; // NOT FINISHED
//private HashSet<int> accountIDs = new HashSet<int>();  // Keeps track of used accountIDs NOT FINISHED
//private Random random = new Random();  // Random number generator NOT FINISHED