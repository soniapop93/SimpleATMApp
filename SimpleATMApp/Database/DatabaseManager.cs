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
    }
}
