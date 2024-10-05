using System.Collections.Generic;

namespace Bank_Console_App
{
    public class User
    {
        private HashSet<int> usedIDs = new HashSet<int>();  // Keeps track of used accountHolderIds
        private Random random = new Random();  // Random number generator
        public bool authorised = false;


        public int accountHolderId { get; private set; } // must be unique
        public string accountHolderFirstName { get; set; }
        public string accountHolderLastName { get; set; }

        public User(string firstName, string lastName)
        {
            accountHolderFirstName = firstName;
            accountHolderLastName = lastName;
            accountHolderId = GenerateUniqueId(); // Generate unique 4-digit ID
        }

        private int GenerateUniqueId()
        {
            int newId;
            do
            {
                newId = random.Next(1000, 10000);  // Generates a number between 1000 and 9999
            }
            while (usedIDs.Contains(newId));  // Ensure it's unique by checking usedIds

            usedIDs.Add(newId);  // Add the unique ID to the set
            return newId;
        }

        public void LoggedOut()
        {
            authorised = false;
            Console.WriteLine("Sad to see you leave so soon!");
            Console.WriteLine("You have been logged out.");
            Console.WriteLine("-------------------------------");
        }

        public void LoggedIn()
        {
            authorised = true;
            Console.WriteLine("-------------------------------");
        }

    }
}
