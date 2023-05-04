using SimpleATMApp.User;

namespace SimpleATMApp.Database
{
    public class MockDataDB
    {
        private Random rnd = new Random();

        public void fillDB(DatabaseManager databaseManager)
        {
            for (int i = 0; i < 100; i++)
            {
                Card card = new Card(i, i, generateCardNumber(), "CardType " + i, DateTime.Parse("2026/04/30"), generatePin());

                Console.WriteLine("Card: " + card.cardNumber + " pin: " + card.pin);

                PersonalInformation personalInformation = new PersonalInformation("FirstName " + i, "LastName " + i, "street Name " + i, "Country " + i, "City " + i, "077777777", "email.user" + i + "@gmail.com");
                UserDetails user = new UserDetails(i, card, generateTransactions(), i * 10000, false, personalInformation);

                databaseManager.insertDataUsers(user);
                databaseManager.insertDataCards(user);

                for (int j = 0; j < user.transactions.Count; j++)
                {
                    databaseManager.insertDataTransactions(card.userID.ToString(), user.transactions[j].transactionDate, user.transactions[j].transactionAmount.ToString(), user.transactions[j].transactionCurrency);
                }
            }
        }

        private string generateCardNumber()
        {
            string cardNumber = "";
            for (int j = 0; j < 16; j++)
            {
                cardNumber += rnd.Next(10).ToString();
            }
            return cardNumber;
        }

        private int generatePin()
        {
            int cardPin = 0;
            for (int j = 0; j < 4; j++)
            {
                cardPin = (cardPin * 10) + rnd.Next(10);
            }
            return cardPin;
        }

        private List<Transaction> generateTransactions()
        {
            List<Transaction> transactionList = new List<Transaction>();
            for (int j = 0; j < rnd.Next(10); j++)
            {
                Transaction transaction = new Transaction("", j.ToString(), DateTime.Parse("2023/04/30").AddDays(j), j * 100, "euro");
                transactionList.Add(transaction);
            }
            return transactionList;
        }
    }
}
