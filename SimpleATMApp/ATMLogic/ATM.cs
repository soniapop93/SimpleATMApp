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
                    "4 - Display money on account \n" +
                    "5 - EXIT";

            if (card != null)
            {
                Console.WriteLine("PIN is correct. Continue...");


                while (true)
                {
                    Console.WriteLine(optionScreen);

                    string optionInput = userInput.getUserInput();
                    string strDefault = "You have selected option: ";

                    string currentMoneyAmount = databaseManager.getAmountMoneyForUser(card.userID.ToString());

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

                            Console.WriteLine("Please input the money amount you want to withdraw: ");

                            try
                            {
                                int moneyToWithdraw = Int32.Parse(userInput.getUserInput());

                                if (((long)Double.Parse(currentMoneyAmount)) > 0 &&
                                    ((long)Double.Parse(currentMoneyAmount) - moneyToWithdraw) >= 0)
                                {
                                    databaseManager.updateAmountMoneyForUserAfterWidrawal(card.userID.ToString(), moneyToWithdraw, (long)Double.Parse(currentMoneyAmount));

                                    Console.WriteLine("You have withdrawn: " + moneyToWithdraw);
                                }
                                else
                                {
                                    Console.WriteLine("Insufficient funds in your account. You can't withdraw: " + moneyToWithdraw.ToString());
                                }

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Incorrect money value. Please try again...");
                            }

                            break;

                        case "4":
                            Console.WriteLine(strDefault + "4 - Display money on account");

                            if (!String.IsNullOrEmpty(currentMoneyAmount))
                            {
                                Console.WriteLine("Current money in your account: " + currentMoneyAmount);
                            }
                            else
                            {
                                Console.WriteLine("You have no money in your account...");
                            }

                            break;

                        case "5":
                            Console.WriteLine(strDefault + "5 - EXIT");
                            break;
                    }
                }
            }
        }
    }
}
