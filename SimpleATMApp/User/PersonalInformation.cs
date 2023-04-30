namespace SimpleATMApp.User
{
    public class PersonalInformation
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string mobilePhone { get; set; }
        public string email { get; set; }

        public PersonalInformation(string firstName, string lastName, string address, string country, string city, string mobilePhone, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.country = country;
            this.city = city;
            this.mobilePhone = mobilePhone;
            this.email = email;
        }
    }
}
