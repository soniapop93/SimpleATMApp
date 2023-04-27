namespace SimpleATMApp.User
{
    public class UserDetails
    {
        public int userID { get; set; }
        public Card card { get; set; }
        public List<Transaction> transactions { get; set; }
        public long cashAvailability { get; set; }
        public bool limitWithdrawal { get; set; }
        public PersonalInformation personalInformation { get; set; }

        public UserDetails(int userID, Card card, List<Transaction> transactions, long cashAvailability, bool limitWithdrawal, PersonalInformation personalInformation)
        {
            this.userID = userID;
            this.card = card;
            this.transactions = transactions;
            this.cashAvailability = cashAvailability;
            this.limitWithdrawal = limitWithdrawal;
            this.personalInformation = personalInformation;
        }
    }
}
