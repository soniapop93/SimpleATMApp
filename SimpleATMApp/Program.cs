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

        Card card1 = new Card(1, 1, "5285294990659975", "Mastercard", DateTime.Parse("2025/05"), 1234);
        Transaction transaction1 = new Transaction("", "1", DateTime.Parse("2023/04/30"), 123, "lei");
        List<Transaction> listTransaction1 = new List<Transaction>();
        listTransaction1.Add(transaction1);
        PersonalInformation personalInformation1 = new PersonalInformation("John", "Smith", "street First 24", "Romania", "Cluj", "0756473823", "john.smith@gmail.com");
        UserDetails user1 = new UserDetails(1, card1, listTransaction1, 50000, false, personalInformation1);

        databaseManager.insertDataUsers(user1);
        databaseManager.insertDataCards(user1);
        databaseManager.insertDataTransactions(user1, DateTime.Parse("2023/04/30"), "123", "lei");



        Console.WriteLine("Data inserted in db for testing");

        ATM atm = new ATM();
        atm.getCardFromUser();




        Console.WriteLine("------------------------ SCRIPT FINISHED ------------------------");
    }
}