using System.Collections.Generic;

namespace Bank_Console_App
{
    internal class Program
    {
        static User currentUser = null!;
        static BankAccount currentAccount = null!;
        static BankAccount recipientAccount = null!;
        static void Main(string[] args)
        {
            //initialization
            UserInput userInput = new UserInput();
            List<User> users = new List<User>(); // Create a list to store multiple User objects

            //Bank app starts
            WelcomePhrase();
            MainMenu(userInput, out userInput.inputNumber, out userInput.inputID, users);

        }



        private static void MainMenu(UserInput userInput, out int inputNumber, out int inputID, List<User> users)
        {
            do
            {
                MainMenuText();
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    case 1:
                        Console.WriteLine("Great! We’re excited to get to know you! Please provide your first and last name.");

                        Console.WriteLine("Please insert your first name:");
                        string firstName = userInput.GetTextInput();

                        Console.WriteLine("Please insert your last name:");
                        string lastName = userInput.GetTextInput();

                        // initiate an object of a user class
                        users.Add(new User(firstName, lastName));
                        break;

                    case 2:
                        if (users.Count == 0)
                        {
                            Console.WriteLine("It looks like you haven’t set up an account with us yet. Let’s get started on creating your first one!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Here is the list of the accounts existing:");
                            foreach (var user in users) // prints out the list of account
                            {
                                Console.WriteLine($"User: {user.accountHolderFirstName} {user.accountHolderLastName}, ID: {user.accountHolderId}");
                            }

                            Console.WriteLine("Kindly enter the 4-digit ID of the account you'd like to sign in to.");
                            userInput.inputID = userInput.GetNumberInput();

                            // Find user by ID
                            currentUser = users.Find(user => user.accountHolderId == userInput.inputID)!;  

                            if (currentUser != null)
                            {
                                currentUser.LoggedIn();
                                Console.WriteLine($"Welcome {currentUser.accountHolderFirstName} {currentUser.accountHolderLastName}!");
                                UserMenu(userInput, currentUser, out userInput.inputNumber, users, currentUser.OwnedAccounts);
                            }
                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                        break;

                    case 9:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;
                }
            }
            while (true);
        }
        private static void UserMenu(UserInput userInput, User user, out int inputNumber, List<User> users, List<BankAccount> bankAccounts)
        {
            do
            {
                UserMenuText();
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    case 1:
                        BankAccountCreationMenu(userInput, user, out userInput.inputNumber, users, currentUser.OwnedAccounts);
                        break;

                    case 2:
                        if (currentUser.OwnedAccounts.Count == 0)
                        {
                            Console.WriteLine("It looks like you haven’t set up an account with us yet. Let’s get started on creating your first one!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Here is your existing accounts:");
                            foreach (var account in currentUser.OwnedAccounts) // prints out the list of account
                            {
                                Console.WriteLine($"Account number: {account.accountNumber}, account type: {account.accountType}.");
                            }

                            Console.WriteLine("Kindly enter the 5-digit account number you'd like to use.");
                            userInput.accountNumber = userInput.GetNumberInput();

                            // Finds account by account number
                            currentAccount = currentUser.OwnedAccounts.Find(account => account.accountNumber == userInput.accountNumber)!;

                            if (currentAccount != null)
                            {
                                Console.WriteLine($"You are inside your {currentAccount.accountType} with account number {currentAccount.accountNumber}.");
                                BankAccountAction(userInput, currentUser, out userInput.inputNumber, users, currentUser.OwnedAccounts);
                            }
                            else
                            {
                                Console.WriteLine("account not found.");
                            }
                        }
                        break;

                    case 3:
                        user.LoggedOut();
                        MainMenu(userInput, out userInput.inputNumber, out userInput.inputID, users);
                        break;

                    case 9:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;

                }
            }
            while (user.authorised == true);
            MainMenu(userInput, out userInput.inputNumber, out userInput.inputID, users);
            
        }


        private static void BankAccountCreationMenu(UserInput userInput, User user, out int inputNumber, List<User> users, List<BankAccount> OwnedAccounts)
        {
            bool proceed = false;
            do
            {
                BankAccountCreationMenuText();
                
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    
                    case 1:
                        Console.WriteLine("You want to create a personal account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        currentUser.OwnedAccounts.Add(new PersonalAccount(currentUser.accountHolderFirstName, currentUser.accountHolderLastName, inputNumber));
                        proceed = true;
                        break;

                    case 2:
                        Console.WriteLine("You want to create a Investment account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        currentUser.OwnedAccounts.Add(new InvestmentAccount(currentUser.accountHolderFirstName, currentUser.accountHolderLastName, inputNumber));
                        proceed = true;
                        break;

                    case 3:
                        Console.WriteLine("You want to create a Savings account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        currentUser.OwnedAccounts.Add(new SavingsAccount(currentUser.accountHolderFirstName, currentUser.accountHolderLastName, inputNumber));
                        proceed = true;
                        break;

                    case 4:
                        proceed = true;
                        break;

                    default :
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;
                }

            }
            while (proceed == false);
            UserMenu(userInput, currentUser, out userInput.inputNumber, users, currentUser.OwnedAccounts);
        }

        private static void BankAccountAction(UserInput userInput, User user, out int inputNumber, List<User> users, List<BankAccount> OwnedAccounts)
        {
            bool proceed = false;
            do
            {
                BankAccountActionMenu();

                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {

                    case 1:
                        currentAccount.CheckBalance();
                        proceed = true;
                        break;
                    
                    case 2:
                        Console.WriteLine("Please insert the amount you would like to deposit:");
                        userInput.inputNumber = userInput.GetNumberInput();
                        currentAccount.Deposit(userInput.inputNumber);
                        proceed = true;
                        break;

                    case 3:
                        Console.WriteLine(" Please insert the amount you would like to withdraw:");
                        userInput.inputNumber = userInput.GetNumberInput();
                        currentAccount.Withdraw(userInput.inputNumber);
                        proceed = true;
                        break;

                    case 4:
                        if (currentUser.OwnedAccounts.Count <= 1)
                        {
                            Console.WriteLine("You do not have enough bank accounts.");
                            proceed = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Here is your existing accounts:");
                            foreach (var account in currentUser.OwnedAccounts)
                            {
                                Console.WriteLine($"Account number: {account.accountNumber}, account type: {account.accountType}.");
                            }

                            do
                            {
                                Console.WriteLine("Please enter the 5-digit account number you'd like to transfer money to.");
                                userInput.accountNumber = userInput.GetNumberInput();
                                if (userInput.accountNumber == currentAccount.accountNumber)
                                {
                                    Console.WriteLine("You can not trasfer to the same account");
                                }
                                else
                                {
                                    // Finds account by account number
                                    recipientAccount = currentUser.OwnedAccounts.Find(account => account.accountNumber == userInput.accountNumber)!;
                                    if (recipientAccount == null)
                                    {
                                        Console.WriteLine("This account does not exist. Please try again");
                                    }
                                }
                            }
                            while (userInput.accountNumber == currentAccount.accountNumber || recipientAccount == null);

                            if (currentAccount != null && recipientAccount!= null)
                            {
                                Console.WriteLine("Please type in the amount you would like to transfer");
                                userInput.inputAmount = userInput.GetNumberInput();
                                if (userInput.inputAmount <= currentAccount.balance)
                                {
                                    currentAccount.TransferMoney(userInput.inputAmount, currentAccount.accountNumber, recipientAccount.accountNumber);
                                    recipientAccount.TransferMoney(userInput.inputAmount, currentAccount.accountNumber, recipientAccount.accountNumber);
                                    Console.WriteLine($"Transfer successful. You transfered {userInput.inputAmount} SEK to account number {recipientAccount.accountNumber} from account number {currentAccount.accountNumber}.");
                                }
                                else 
                                {
                                    Console.WriteLine("Insufficient funds.");
                                }

                                BankAccountAction(userInput, currentUser, out userInput.inputNumber, users, currentUser.OwnedAccounts);
                            }
                            else
                            {
                                Console.WriteLine("account not found.");
                            }
                        }
                        proceed = true;
                        break;

                    case 5:
                        Console.WriteLine("Here is the transfer history of your account:");
                        currentAccount.CheckTransferHistory();
                        proceed = true;
                        break;

                    case 6:
                        UserMenu(userInput, currentUser, out userInput.inputNumber, users, OwnedAccounts);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;
                }

            }
            while (proceed == false);
            BankAccountAction(userInput, currentUser, out userInput.inputNumber, users, OwnedAccounts);
        }

        //Menus 
        private static void WelcomePhrase() 
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("Welcome to Eddie's Bank, where your financial journey is in great hands!");
            Console.WriteLine("We’re thrilled to have you with us, and as a new customer, rest assured—exciting things are ahead!");
            Console.WriteLine("====================================================================================================");

        }

        private static void MainMenuText() 
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Open a new bank account");
            Console.WriteLine("2. Choose an existing account");
            Console.WriteLine("9. Exit");
        }

        private static void UserMenuText() 
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Create a new bank account");
            Console.WriteLine("2. Choose an existing account"); 
            Console.WriteLine("3. Log out");
            Console.WriteLine("9. Exit Application");
        }

        private static void BankAccountCreationMenuText()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Which type of account would you like to create?");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Personal account");
            Console.WriteLine("2. Investment account");
            Console.WriteLine("3. Saving account");
            Console.WriteLine("4. Go back");
        }

        private static void BankAccountActionMenu()
        {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("What would you like to do.");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Deposit money");
            Console.WriteLine("3. Withdraw money");
            Console.WriteLine("4. Transfer money");
            Console.WriteLine("5. Transfer history");
            Console.WriteLine("6. Go back");
        }
    }
}
