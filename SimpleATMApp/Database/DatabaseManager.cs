using SimpleATMApp.User;
using System.Data.SQLite;

namespace SimpleATMApp.Database
{
    public class DatabaseManager
    {
        private SQLiteConnection sqLiteConnection;

        public DatabaseManager()
        {
            generateDB("C:\\Users\\" + System.Environment.UserName + "\\Desktop", "ATMbankDB");
        }
        private void generateDB(string path, string fileName)
        {
            string fileNamePath = path + @"\" + fileName + ".db";
            createConnection(fileNamePath);

            Console.WriteLine("Database is created successfully");
            createTableUsers();
            createTableCards();
            createTableTransactions();
        }

        private void createConnection(string fileNamePath)
        {
            string strConnection = String.Format("Data Source={0};Version=3;New=True;Compress=True;", fileNamePath);
            sqLiteConnection = new SQLiteConnection(strConnection);
        }

        private void createTableUsers()
        {
            string strSql = "CREATE TABLE IF NOT EXISTS Users " +
                "(id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "first_name TEXT," +
                "last_name TEXT," +
                "address TEXT," +
                "country TEXT," +
                "city TEXT," +
                "mobile_phone TEXT," +
                "email TEXT," +
                "card_id INT," +
                "cash_availability TEXT," +
                "limitWithdrawal INT," +
                "transaction_ids TEXT)"; 
            
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strSql;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        private void createTableCards()
        {
            string strSql = "CREATE TABLE IF NOT EXISTS Cards " +
                "(id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "user_id INT," +
                "card_number TEXT," +
                "card_type TEXT," +
                "expiration_date TEXT," +
                "pin TEXT)";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strSql;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        private void createTableTransactions()
        {
            string strSql = "CREATE TABLE IF NOT EXISTS Transactions " +
                "(id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "user_id INT," +
                "transaction_date TEXT," +
                "transaction_Amount TEXT," +
                "transaction_currency TEXT)";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strSql;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataUsers(UserDetails user)
        {
            List<string> transactionListIds = new List<string>();

            for (int i = 0; i < user.transactions.Count; i++)
            {
                transactionListIds.Add(user.transactions[i].transactionID);            
            }


            string strData = "INSERT INTO Users " +
                "(first_name," +
                "last_name," +
                "address," +
                "country," +
                "city," +
                "mobile_phone," +
                "email," +
                "card_id," +
                "cash_availability," +
                "limitWithdrawal," +
                "transaction_ids) VALUES " +
                "('" +
                user.personalInformation.firstName + "','" +
                user.personalInformation.lastName + "','" +
                user.personalInformation.address + "','" +
                user.personalInformation.country + "','" +
                user.personalInformation.city + "','" +
                user.personalInformation.mobilePhone + "','" +
                user.personalInformation.email + "'," + 
                user.card.cardID + ",'" + 
                user.cashAvailability + "','" + 
                user.limitWithdrawal + "','" + 
                String.Join("-", transactionListIds) + "');";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataCards(UserDetails user)
        {
            string strData = "INSERT INTO Cards " +
               "(user_id," + 
               "card_number," +
               "card_type," +
               "expiration_date," +
               "pin) VALUES " +
               "(" +
               user.userID + ",'" +
               user.card.cardNumber + "','" +
               user.card.cardType + "','" +
               user.card.expirationDate + "','" +
               user.card.pin.ToString() + "');";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataTransactions(UserDetails user, DateTime transactionDate, string transactionAmount, string transactionCurrency)
        {
            string strData = "INSERT INTO Transactions " +
               "(user_id," +
               "transaction_date," +
               "transaction_Amount," +
               "transaction_currency) VALUES " +
               "('" +
               user.userID.ToString() + "','" +
               transactionDate.ToString() + "','" +
               transactionAmount + "','" +
               transactionCurrency + "');";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public List<Transaction> getTransactionsForUser(string userID)
        {
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();

            string strData = "SELECT * FROM Transactions WHERE user_id = " + userID + ";";
            sqLiteCommand.CommandText = strData;
            SQLiteDataReader allDBdata = sqLiteCommand.ExecuteReader();

            List<Transaction> transactions = displayTransactionsForUser(allDBdata);
            sqLiteConnection.Close();

            return transactions;
        }

        private List<Transaction> displayTransactionsForUser(SQLiteDataReader allDBdata)
        {
            List<Transaction> transactions = new List<Transaction>();
            while (allDBdata.Read())
            {
                Transaction transaction = new Transaction(
                    allDBdata[0].ToString(), // transactionID
                    allDBdata[1].ToString(), // userID
                    DateTime.Parse(allDBdata[2].ToString()), // transactionDate
                    (long)Double.Parse(allDBdata[3].ToString()), // transactionAmount
                    allDBdata[4].ToString()); // transactionCurrency

                transactions.Add(transaction);
            }
            return transactions;
        }

        public string getAmountMoneyForUser(string userID)
        {
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();

            string strData = "SELECT * FROM Users WHERE id = " + userID + ";";

            sqLiteCommand.CommandText = strData;
            SQLiteDataReader allDBdata = sqLiteCommand.ExecuteReader();

            string currentMoneyAmount = displayAmountMoneyForUser(allDBdata);

            sqLiteConnection.Close();

            return currentMoneyAmount;
        }

        private string displayAmountMoneyForUser(SQLiteDataReader allDBdata)
        {
            string currentMoneyAmount = "";
            while (allDBdata.Read())
            {
                currentMoneyAmount = allDBdata[9].ToString();
            }
            return currentMoneyAmount;
        }

        public void updateAmountMoneyForUserAfterWidrawal(string userID, int moneyWithdrawn, long currentAmountMoney)
        {
            long newMoneyAmount = currentAmountMoney - moneyWithdrawn;

            string strData = "UPDATE Users SET cash_availability = '" + newMoneyAmount.ToString() + "' WHERE id = " + userID + ";";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public PersonalInformation getUserInfoFromUser(string userID)
        {
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();

            string strData = "SELECT * FROM Users WHERE id = " + userID + ";";
            sqLiteCommand.CommandText = strData;
            SQLiteDataReader allDBdata = sqLiteCommand.ExecuteReader();

            PersonalInformation personalInformation = displayUserInfo(allDBdata);

            sqLiteConnection.Close();

            return personalInformation;
        }

        private PersonalInformation displayUserInfo(SQLiteDataReader allDBdata)
        {
            while (allDBdata.Read())
            {
                PersonalInformation personalInformation = new PersonalInformation(
                    allDBdata[1].ToString(), //first name
                    allDBdata[2].ToString(), //last name
                    allDBdata[3].ToString(), //address
                    allDBdata[4].ToString(), //country
                    allDBdata[5].ToString(), //city
                    allDBdata[6].ToString(), //mobile phone
                    allDBdata[7].ToString()); //email
                return personalInformation;
            }
            return null;
        }

        public Card getCardForUser(string cardNumber, int cardPin)
        {
            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();

            string strData = "SELECT * FROM Cards WHERE card_number = " + cardNumber + ";";
            sqLiteCommand.CommandText = strData;
            SQLiteDataReader allDBdata = sqLiteCommand.ExecuteReader();

            Card card = checkCardForUser(allDBdata, cardPin);
            sqLiteConnection.Close();

            return card;
        }

        private Card checkCardForUser(SQLiteDataReader allDBdata, int cardPin) 
        {
            while (allDBdata.Read())
            {
                if(checkPinCard(Int32.Parse(allDBdata[5].ToString()), cardPin))
                {
                    Card card = new Card(
                        Int32.Parse(allDBdata[0].ToString()), 
                        Int32.Parse(allDBdata[1].ToString()), 
                        allDBdata[2].ToString(), 
                        allDBdata[3].ToString(), 
                        DateTime.Parse(allDBdata[4].ToString()), 
                        Int32.Parse(allDBdata[5].ToString()));
                    return card;
                }
            }
            return null;
            
        }

        private bool checkPinCard(int dbPin, int cardPin)
        {
            if (dbPin == cardPin)
            {
                return true;
            }
            return false;
        }
    }
}
