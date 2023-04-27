namespace SimpleATMApp.User
{
    public class Transaction
    {
        public string transactionID { get; set; }
        public DateTime transactionDate { get; set; }
        public long transactionAmount { get; set; }
        public string transactionCurrency { get; set; }

        public Transaction
            (string transactionID, 
            DateTime transactionDate, 
            long transactionAmount, 
            string transactionCurrency)
        {
            this.transactionID = transactionID;
            this.transactionDate = transactionDate;
            this.transactionAmount = transactionAmount;
            this.transactionCurrency = transactionCurrency;
        }
    }
}
