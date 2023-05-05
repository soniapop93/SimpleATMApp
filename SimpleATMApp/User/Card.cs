namespace SimpleATMApp.User
{
    public class Card
    {
        public int cardID { get; set; }
        public int userID { get; set; }
        public string cardNumber { get; set; }
        public string cardType { get; set; }
        public DateTime expirationDate { get; set; }
        public string hashedPin { get; set; }

        public Card(int cardID, int userID, string cardNumber, string cardType, DateTime expirationDate, string hashedPin)
        {
            this.cardID = cardID;
            this.userID = userID;
            this.cardNumber = cardNumber;
            this.cardType = cardType;
            this.expirationDate = expirationDate;
            this.hashedPin = hashedPin;
        }
    }

}
