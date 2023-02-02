namespace BookStore.Models.Entities
{
    public class Person : EntityLogBase
    {
        public long PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email {get; set;}

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public Person(string firstName, string lastName, string address) : this(firstName, lastName)
        {
            Address = address;
        }
        public Person(string firstName, string lastName, string address, string phoneNumber, string email)
            : this(firstName, lastName, address)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}