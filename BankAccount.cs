using System.Collections.Generic;

namespace Bank_Console_App
{
    public abstract class BankAccount
    {
        private static HashSet<int> usedAccountNumbers = new HashSet<int>();  // Keeps track of used account numbers
        private static Random random = new Random();  // Random number generator

        string? accountHolderFirstName {  get; set; }
        string? accountHolderLastName { get; set; }
        public int accountNumber { get; set; }
        public string? accountType { get; set; }
        public int balance { get; set; }

        // creates a list of tuples
        List<(DateTime time, int amount, int senderAccount, int recipientAccount)> TransferHistory = new List<(DateTime, int, int, int)>(); 

        public BankAccount(string FirstName, string LastName, string Accounttype, int InitialBalance)
        {
            accountHolderFirstName = FirstName;
            accountHolderLastName = LastName;
            accountNumber = GenerateUniqueAccountNumber();
            accountType = Accounttype;
            balance = InitialBalance;
        }

        private int GenerateUniqueAccountNumber()
        {
            int newAccountNumber;
            do
            {
                newAccountNumber = random.Next(10000, 99999);  // Generates a number between 10000 and 99999
            }
            while (usedAccountNumbers.Contains(newAccountNumber));  // Ensure it's unique

            usedAccountNumbers.Add(newAccountNumber);  // Add the unique account number to the set
            return newAccountNumber;
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your current balance is {balance} SEK");
            
        }

        public void Deposit(int amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"{amount} SEK deposited. New balance: {balance} SEK");
            }
            else
            {
                Console.WriteLine("The amount can not be negative number");
            }
        }

        public void Withdraw(int amount)
        {
            if (amount > 0)
            {
                if (amount <= balance)
                {
                    balance -= amount;
                    Console.WriteLine($"{amount} SEK withdrawn. New balance: {balance} SEK");
                }
                else
                {
                    Console.WriteLine("Insufficient funds.");
                }
            }
            else
            {
                Console.WriteLine("The amount can not be negative number");
            }
        }
        public void TransferMoney(int amount, int senderAccount, int recipientAccount)
        {
            
            if (senderAccount == accountNumber)
            {
                balance -= amount;
            }
            else if (recipientAccount == accountNumber)
            {
                balance += amount;
            }
            TransferHistory.Add((DateTime.Now ,amount, senderAccount, recipientAccount));
        }
        public void CheckTransferHistory()
        {
            foreach(var transferEvent in TransferHistory)
            {
                DateTime Time = transferEvent.time;
                int Amount = transferEvent.amount;
                int SenderAccount = transferEvent.senderAccount;
                int RecipientAccount = transferEvent.recipientAccount;

                Console.WriteLine($"Date: {Time}, Amount: {Amount} SEK, From: {SenderAccount}, To: {RecipientAccount}");
            }
        }


    }
}