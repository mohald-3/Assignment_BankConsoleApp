using System.Collections.Generic; // NOT FINISHED

namespace Bank_Console_App
{
    public abstract class BankAccount
    {
        string? accountHolderName;
        public int accountNumber;
        public string? accountType;
        public int balance;
        List<(DateTime time, int amount, int fromAccount, int toAccount )> TransfareEvent = new List<(DateTime, int, int, int)>() ; // creates a tuple in a list

        private HashSet<int> accountIDs = new HashSet<int>();  // Keeps track of used accountIDs NOT FINISHED
        private Random random = new Random();  // Random number generator NOT FINISHED

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
