
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
        List<(DateTime time, int amount, int senderAccount, int recipientAccount)> TransferHistory = new List<(DateTime, int, int, int)>(); 

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
            Console.WriteLine($"Your current balance is {balance} SEK");
            
        }

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"{amount} SEK deposited. New balance: {balance} SEK");
        }

        public void Withdraw(int amount)
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