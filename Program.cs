using System.Collections.Generic;

namespace Bank_Console_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //initialization
            UserInput userInput = new UserInput();
            List<User> users = new List<User>(); // Create a list to store multiple User objects

            //Bank app starts
            WelcomePhrase();
            MainManu(userInput, out userInput.inputNumber, out userInput.inputID, users);

        }



        private static void MainManu(UserInput userInput, out int inputNumber, out int inputID, List<User> users)
        {
            do
            {
                MainManuText();
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    case 1:
                        Console.WriteLine("Great! We’re excited to get to know you! Please provide your first and last name.");
                        //need to create a class user with the information provided
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
                            Console.WriteLine("here is the list of the accounts existing:");
                            foreach (var user in users) // prints out the list of account
                            {
                                Console.WriteLine($"User: {user.accountHolderFirstName} {user.accountHolderLastName}, ID: {user.accountHolderId}");
                            }

                            Console.WriteLine("Kindly enter the 4-digit ID of the account you'd like to sign in to.");
                            userInput.inputID = userInput.GetNumberInput();

                            User foundUser = users.Find(user => user.accountHolderId == userInput.inputID)!;  // Find user by ID

                            if (foundUser != null)
                            {
                                foundUser.LoggedIn();
                                Console.WriteLine($"Welcome {foundUser.accountHolderFirstName} {foundUser.accountHolderLastName}!");
                                UserManu(userInput, foundUser, out userInput.inputNumber, users);

                                // now we should transfer to another switch list with options for the specific user.
                                // for that we need to save inputID value outside of this fucntion to be able to use it for the upcoming function.
                                // for we should exit the loop function by either changing the state of the value true to false so we skip this manu.

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
        private static void UserManu(UserInput userInput, User user, out int inputNumber, List<User> users) // consider changing inputID into bank accounts from and to, and List<users> will not be used here anymore but another one rather
        {
            do
            {
                UserManuText();
                inputNumber = userInput.GetNumberInput();
                switch (inputNumber)
                {
                    //Console.WriteLine("1. Create a new bank account"); // leads to BankAccountManu list

                    //Console.WriteLine("2. Check existing accounts");

                    //Console.WriteLine("3. Check balance");

                    //Console.WriteLine("4. Deposit money");

                    //Console.WriteLine("5. Withdraw money");

                    //Console.WriteLine("6. Transfer money between accounts");

                    
                    case 7:
                        user.LoggedOut();
                        MainManu(userInput, out userInput.inputNumber, out userInput.inputID, users);
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

            MainManu(userInput, out userInput.inputNumber, out userInput.inputID, users);
            
        }

        private static void WelcomePhrase() // resolved
        {
            Console.WriteLine("Welcome to Eddie's Bank, where your financial journey is in great hands!");
            Console.WriteLine("We’re thrilled to have you with us, and as a new customer, rest assured—exciting things are ahead!");
        }

        private static void MainManuText() //resolved
        {
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Open a new bank account");
            Console.WriteLine("2. Choose an existing account");
            Console.WriteLine("9. Exit");
        }

        private static void UserManuText()
        {
            Console.WriteLine("How can we help you today?");
            Console.WriteLine("1. Create a new bank account"); // leads to BankAccountManu list
            Console.WriteLine("2. Check existing accounts");
            Console.WriteLine("3. Check balance");
            Console.WriteLine("4. Deposit money");
            Console.WriteLine("5. Withdraw money");
            Console.WriteLine("6. Transfer money between accounts");
            Console.WriteLine("7. Log out");
            Console.WriteLine("9. Exit Application");
        }

        private static void BankAccountManuText()
        {
            Console.WriteLine("Which type of account would you like to create?");
            Console.WriteLine("1. Personal account");
            Console.WriteLine("2. Investment account");
            Console.WriteLine("3. Saving account");
            //maybe add go back?
        }
    }
}
