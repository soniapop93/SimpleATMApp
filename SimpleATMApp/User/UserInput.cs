﻿namespace SimpleATMApp.User
{
    public class UserInput
    {
        public string getUserInput()
        {
            string strInputUser = Console.ReadLine();

            if (!String.IsNullOrEmpty(strInputUser))
            {
                return strInputUser;
            }
            return "";
        }
    }
}
