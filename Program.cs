using System.Collections.Generic;

namespace Bank_Console_App
{
    internal class Program
    {
        static User foundUser = null!;
        static BankAccount foundAccount = null!;
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

                            foundUser = users.Find(user => user.accountHolderId == userInput.inputID)!;  // Find user by ID

                            if (foundUser != null)
                            {
                                foundUser.LoggedIn();
                                Console.WriteLine($"Welcome {foundUser.accountHolderFirstName} {foundUser.accountHolderLastName}!");
                                UserMenu(userInput, foundUser, out userInput.inputNumber, users, foundUser.OwnedAccounts);
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
        // add these later -- out int fromAccount, out int toAccount , List<accounts> accounts --
        private static void UserMenu(UserInput userInput, User user, out int inputNumber, List<User> users, List<BankAccount> bankAccounts)
        {
            do
            {
                UserMenuText();
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    // 1. Create a new bank account, leads to BankAccountMenu list
                    case 1:

                        BankAccountCreationMenu(userInput, user, out userInput.inputNumber, users, foundUser.OwnedAccounts);
                        break;

                    // 2. choose an existing account
                    case 2:

                        if (foundUser.OwnedAccounts.Count == 0)
                        {
                            Console.WriteLine("It looks like you haven’t set up an account with us yet. Let’s get started on creating your first one!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Here is your existing accounts:");
                            foreach (var account in foundUser.OwnedAccounts) // prints out the list of account
                            {
                                Console.WriteLine($"Account number: {account.accountNumber}, account type: {account.accountType}.");
                            }

                            Console.WriteLine("Kindly enter the 5-digit account number you'd like to use.");
                            userInput.accountNumber = userInput.GetNumberInput();

                            // Finds account by account number
                            foundAccount = foundUser.OwnedAccounts.Find(account => account.accountNumber == userInput.accountNumber)!;

                            if (foundAccount != null)
                            {

                                Console.WriteLine($"You are inside your {foundAccount.accountType} with account number {foundAccount.accountNumber}.");
                                BankAccountAction(userInput, foundUser, out userInput.inputNumber, users, foundUser.OwnedAccounts);
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


        private static void BankAccountCreationMenu(UserInput userInput, User user, out int inputNumber, List<User> users, List<BankAccount> OwnedAccounts) //resolved
        {
            bool proceed = false;
            do
            {
                BankAccountCreationMenuText();
                
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    
                    //Console.WriteLine("1. Personal account");
                    case 1:
                        Console.WriteLine("You want to create a personal account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        foundUser.OwnedAccounts.Add(new PersonalAccount(foundUser.accountHolderFirstName, foundUser.accountHolderLastName, 10500, inputNumber));
                        // auto generate bankaccountID
                        proceed = true;
                        break;

                    //Console.WriteLine("2. Investment account");
                    case 2:
                        Console.WriteLine("You want to create a Investment account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        foundUser.OwnedAccounts.Add(new InvestmentAccount(foundUser.accountHolderFirstName, foundUser.accountHolderLastName, 10400, inputNumber));
                        // auto generate bankaccountID
                        proceed = true;
                        break;

                    //Console.WriteLine("3. Savings account");
                    case 3:
                        Console.WriteLine("You want to create a Savings account. Please insert the amount of money you would like to start the account with");
                        inputNumber = userInput.GetNumberInput();
                        foundUser.OwnedAccounts.Add(new SavingsAccount(foundUser.accountHolderFirstName, foundUser.accountHolderLastName, 10300, inputNumber));
                        // auto generate bankaccountID
                        proceed = true;
                        break;

                    //Console.WriteLine("4. Go back");
                    case 4:
                        proceed = true;
                        break;

                    default :
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;
                }

            }
            while (proceed != true);
            UserMenu(userInput, foundUser, out userInput.inputNumber, users, foundUser.OwnedAccounts);
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

                    //Console.WriteLine("1. Check balance");
                    case 1:
                        foundAccount.CheckBalance();
                        proceed = true;
                        break;

                    //Console.WriteLine("2. Deposit money");
                    case 2:
                        Console.WriteLine("Please insert the amount you would like to deposit:");
                        userInput.inputNumber = userInput.GetNumberInput();
                        foundAccount.Deposit(userInput.inputNumber);
                        Console.WriteLine($"You successfully added {userInput.inputNumber} to your account. Your current balance is {foundAccount.balance}");
                        proceed = true;
                        break;

                    //Console.WriteLine("3. Withdraw money");
                    case 3:
                        Console.WriteLine(" Please insert the amount you would like to withdraw:");
                        userInput.inputNumber = userInput.GetNumberInput();
                        foundAccount.Withdraw(userInput.inputNumber);
                        Console.WriteLine($"You successfully added {userInput.inputNumber} to your account. Your current balance is {foundAccount.balance}");
                        proceed = true;
                        break;

                    //Console.WriteLine("4. Transfer money");
                    case 4:
                        Console.WriteLine("Please type in which account you want to transfer money to, use the 5 digit account number.");
                        int toAccount = userInput.GetNumberInput();
                        Console.WriteLine("Please type in the amount you would like to transfer");
                        Console.WriteLine($"your current balance is {foundAccount.balance}.");
                        int amount = userInput.GetNumberInput();

                        break;

                    //Console.WriteLine("5. Go back");
                        case 5:
                        proceed = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid number");
                        break;
                }

            }
            while (proceed != true);
            BankAccountAction(userInput, foundUser, out userInput.inputNumber, users, OwnedAccounts);
        }

        private static void WelcomePhrase() // resolved
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine("Welcome to Eddie's Bank, where your financial journey is in great hands!");
            Console.WriteLine("We’re thrilled to have you with us, and as a new customer, rest assured—exciting things are ahead!");
            Console.WriteLine("====================================================================================================");

        }

        private static void MainMenuText() //resolved
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Open a new bank account");
            Console.WriteLine("2. Choose an existing account");
            Console.WriteLine("9. Exit");
        }

        private static void UserMenuText() 
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Create a new bank account"); // leads to BankAccountMenu list
            Console.WriteLine("2. Choose an existing account"); // leads to BankAccountActionMenu list
            Console.WriteLine("3. Log out");
            Console.WriteLine("9. Exit Application");
        }

        private static void BankAccountCreationMenuText() //resolved
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Which type of account would you like to create?");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Personal account");
            Console.WriteLine("2. Investment account");
            Console.WriteLine("3. Saving account");
            Console.WriteLine("4. Go back");
        }

        private static void BankAccountActionMenu()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("What would you like to do.");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Deposit money");
            Console.WriteLine("3. Withdraw money");
            Console.WriteLine("4. Transfer money");
            Console.WriteLine("5. Go back");
        }
    }
}
