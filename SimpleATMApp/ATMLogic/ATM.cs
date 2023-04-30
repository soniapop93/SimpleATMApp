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

                Console.WriteLine("Please select one of the options: \n" +
                    "1 - Display personal information \n" +
                    "2 - Display transactions \n" +
                    "3 - Withdraw money \n" +
                    "4 - Display money on account\n" +
                    "5 - EXIT");

                string optionInput = userInput.getUserInput();

                switch (optionInput)
                {
                    default:
                        Console.WriteLine("No correct option selected");
                        break;

                    case "1":
                        Console.WriteLine("You have selected option 1 - Display personal information");
                        //TODO: implement functionality
                        break;


                }
                    


            }

        }

    }
}
