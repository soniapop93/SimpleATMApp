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
                "(userID INT," +
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
                "(userID INT," +
                "transaction_date TEXT," +
                "transaction_ammount TEXT," +
                "transaction_currency TEXT)";

            sqLiteConnection.Open();
            SQLiteCommand sqLiteCommand = sqLiteConnection.CreateCommand();
            sqLiteCommand.CommandText = strSql;
            sqLiteCommand.ExecuteNonQuery();
            sqLiteConnection.Close();
        }
    }
}
