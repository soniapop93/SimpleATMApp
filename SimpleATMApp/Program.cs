using SimpleATMApp.ATMLogic;
using SimpleATMApp.Database;
using SimpleATMApp.User;
using System.Net;

public class Program
{
    public static void Main(string[] args)
    {
        /*
           =============================================================
           =============================================================
             

           =============================================================
           =============================================================
        */

        Console.WriteLine("------------------------ SCRIPT STARTED ------------------------");

        // Create DB connection
        DatabaseManager databaseManager = new DatabaseManager();

        //ONLY FOR TESTING
        MockDataDB mockDataDB = new MockDataDB();
        mockDataDB.fillDB(databaseManager);
       

        //Console.WriteLine("Data inserted in db for testing");

        ATM atm = new ATM();
        atm.getCardFromUser();



        Console.WriteLine("------------------------ SCRIPT FINISHED ------------------------");
    }
}