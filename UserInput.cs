using System.Text.RegularExpressions;

namespace Bank_Console_App
{
    public class UserInput
    {
        public string inputText;
        public int inputNumber;
        public int inputID;

        // Method to get validated text input
        public string GetTextInput()
        {
            string? input;
            Regex regex = new Regex("^[a-zA-Z ]+$"); // Allows letters and spaces only

            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again:");
                }
                else if(!regex.IsMatch(input))
                {
                    Console.WriteLine("Input must contain only letters. Please try again:");
                    input = null; // Set input to null to force the loop to continue
                }
            }
            while (string.IsNullOrEmpty(input));

            return input;
        }

        // Method to get validated number input
        public int GetNumberInput()
        {
            string? input;
            int number;

            do
            {
                input = Console.ReadLine();

                if (int.TryParse(input, out number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number:");
                }
            }
            while (true);
        }

    }
}