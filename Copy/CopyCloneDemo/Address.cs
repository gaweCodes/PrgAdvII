namespace CopyCloneDemo
{
    internal class Address
    {
        public int PostalCode { get; set; }
        public string City { get; set; }
        public Address(int postalCode, string city)
        {
            PostalCode = postalCode;
            City = city;
        }
    }
}
