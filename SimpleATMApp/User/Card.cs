namespace SimpleATMApp.User
{
    public class Card
    {
        public string cardNumber { get; set; }
        public string cardType { get; set; }
        public DateTime expirationDate { get; set; }
        public int pin { get; set; }

        public Card(string cardNumber, string cardType, DateTime expirationDate, int pin)
        {
            this.cardNumber = cardNumber;
            this.cardType = cardType;
            this.expirationDate = expirationDate;
            this.pin = pin;
        }
    }

}
