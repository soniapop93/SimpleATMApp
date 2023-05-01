using SimpleATMApp.Database;
using SimpleATMApp.User;

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

            string optionScreen = "Please select one of the options: \n" +
                    "1 - Display personal information \n" +
                    "2 - Display transactions \n" +
                    "3 - Withdraw money \n" +
                    "4 - Display money on account\n" +
                    "5 - EXIT";

            if (card != null)
            {
                Console.WriteLine("PIN is correct. Continue...");

                Console.WriteLine(optionScreen);

                string optionInput = userInput.getUserInput();
                string strDefault = "You have selected option: ";

                switch (optionInput)
                {
                    default:
                        Console.WriteLine("No correct option selected");
                        break;

                    case "1":
                        Console.WriteLine(strDefault + "1 - Display personal information");
                        PersonalInformation personalInformation = databaseManager.getUserInfoFromUser(card.userID.ToString());

                        if (personalInformation != null)
                        {
                            Console.WriteLine(String.Format("Your personal information is: \n" +
                                "First name: {0}\n" +
                                "Last name: {1}\n" +
                                "Address: {2}\n" +
                                "Country: {3}\n" +
                                "City: {4}\n" +
                                "Mobile phone: {5}\n" +
                                "E-mail: {6}", 
                                personalInformation.firstName, 
                                personalInformation.lastName, 
                                personalInformation.address, 
                                personalInformation.country, 
                                personalInformation.city, 
                                personalInformation.mobilePhone, 
                                personalInformation.email));

                            //TODO: implemenent to return to main screen
                        }
                        else
                        {
                            Console.WriteLine("No personal information found about user. Contact bank for more information");
                            Console.WriteLine(optionScreen);

                        }
                        break;

                    case "2":
                        Console.WriteLine(strDefault + "2 - Display transactions");

                        List<Transaction> transactions = databaseManager.getTransactionsForUser(card.userID.ToString());

                        if (transactions.Count > 0)
                        {
                            for (int i = 0; i < transactions.Count; i++)
                            {
                                Console.WriteLine("**************************************");
                                string strTransaction = String.Format(
                                    "Transaction ID: {0}\n" +
                                    "Transaction Date: {1}\n" +
                                    "Transaction Amount: {2}\n" +
                                    "Transaction Currency: {3}", 
                                    transactions[i].transactionID, 
                                    transactions[i].transactionDate.ToString(), 
                                    transactions[i].transactionAmount.ToString(), 
                                    transactions[i].transactionCurrency);

                                Console.WriteLine(strTransaction);
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no transaction history...");
                        }
                        break;

                    case "3":
                        Console.WriteLine(strDefault + "3 - Withdraw money");
                        //TODO: implement functionality
                        break;

                    case "4":
                        Console.WriteLine(strDefault + "4 - Display money on account");
                        //TODO: implement functionality
                        break;

                    case "5":
                        Console.WriteLine(strDefault + "5 - EXIT");
                        //TODO: implement functionality
                        break;
                }
            }
        }
    }
}
