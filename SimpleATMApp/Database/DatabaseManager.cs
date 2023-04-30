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
                "(user_id INT PRIMARY KEY," +
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
                "(card_id INT PRIMARY KEY," +
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
                "(transaction_id INT PRIMARY KEY," +
                "user_id INT," +
                "transaction_date TEXT," +
                "transaction_ammount TEXT," +
                "transaction_currency TEXT)";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strSql;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataUsers(UserDetails user)
        {
            string strData = "INSERT INTO Users " +
                "(first_name TEXT," +
                "last_name TEXT," +
                "address TEXT," +
                "country TEXT," +
                "city TEXT," +
                "mobile_phone TEXT," +
                "email TEXT," +
                "card_id INT," +
                "cash_availability TEXT," +
                "limitWithdrawal INT," +
                "transaction_ids TEXT) VALUES " +
                "('" +
                user.personalInformation.firstName + "','" +
                user.personalInformation.lastName + "','" +
                user.personalInformation.address + "','" +
                user.personalInformation.country + "','" +
                user.personalInformation.city + "','" +
                user.personalInformation.mobilePhone + "','" +
                user.personalInformation.email + "'," + 
                user.card.cardID + ",'" + 
                user.cashAvailability + "'," + 
                user.limitWithdrawal + ",'" + 
                String.Join(",", user.transactions) + "');";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataCards(UserDetails user)
        {
            string strData = "INSERT INTO Cards " +
               "(user_id INT," + 
               "card_number TEXT," +
               "card_type TEXT," +
               "expiration_date TEXT," +
               "pin TEXT) VALUES " +
               "(" +
               user.userID + ",'" +
               user.card.cardNumber + "','" +
               user.card.cardType + "','" +
               user.card.expirationDate + "','" +
               user.card.pin + "');";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strData;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }

        public void insertDataTransactions(UserDetails user, DateTime transactionDate, string transactionAmmount, string transactionCurrency)
        {
            string strData = "INSERT INTO Transactions " +
               "(user_id INT," +
               "transaction_date TEXT," +
               "transaction_ammount TEXT," +
               "transaction_currency TEXT) VALUES " +
               "(" +
               transactionDate + ",'" +
               transactionAmmount + "','" +
               transactionCurrency + "');";

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
                    allDBdata[5].ToString(), //country
                    allDBdata[6].ToString(), //city
                    allDBdata[7].ToString(), //mobile phone
                    allDBdata[8].ToString()); //email
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
                if(checkPinCard((Int32)(allDBdata[5]), cardPin))
                {
                    Card card = new Card(
                        (Int32)allDBdata[0], 
                        (Int32)allDBdata[1], 
                        allDBdata[2].ToString(), 
                        allDBdata[3].ToString(), 
                        DateTime.Parse(allDBdata[4].ToString()), 
                        (Int32)allDBdata[5]);
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
