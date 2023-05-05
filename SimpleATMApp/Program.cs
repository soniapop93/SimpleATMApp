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

            Simple ATM Software

            This simple project will essentially create a simulation of an ATM within a Windows program. 
            Just like an ATM, the program should have at least the following features:

                [X] -> Checking whether an input – such as an ATM card (a debit/credit card number) – is recorded correctly
                [X] -> Verifying the user by asking for a PIN
                [ ] -> Save the PIN in DB as SHA256
                [X] -> In case of negative verification, logging out the user
                [X] -> In case of positive verification, showing multiple options, including cash availability, the previous transactions, cash withdrawal, personal information
                [X] -> Giving the user the ability to withdraw up to $1,000 worth of cash in one transaction, with total transactions limited to ten per day.
                
           =============================================================
           =============================================================
        */

        Console.WriteLine("------------------------ SCRIPT STARTED ------------------------");

        // ONLY FOR TESTING -> can be used to add some testing data in database

        //DatabaseManager databaseManager = new DatabaseManager();
        //MockDataDB mockDataDB = new MockDataDB();
        //mockDataDB.fillDB(databaseManager);
        //Console.WriteLine("Data inserted in db for testing");

        ATM atm = new ATM();
        atm.getCardFromUser();

        Console.WriteLine("------------------------ SCRIPT FINISHED ------------------------");
    }
}