using SimpleATMApp.Database;
using SimpleATMApp.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleATMApp.ATMLogic
{
    public class ATM
    {
        UserInput userInput = new UserInput();
        DatabaseManager databaseManager = new DatabaseManager();
        public void getCardFromUser()
        {
            Console.WriteLine("Please insert card number: ");
            string inputCardNumber = userInput.getUserInput();

            Console.WriteLine("Please insert PIN: ");
            int inputPin = Int32.Parse(userInput.getUserInput());

            Card card = databaseManager.getCardForUser(inputCardNumber, inputPin);

            if (card != null)
            {
                Console.WriteLine("PIN is correct. Continue...");
                //TODO: implement to show transactions, personal info, and withdraw money
            }

        }

    }
}
