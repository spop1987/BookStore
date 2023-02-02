using BookStore.DataStore;
using BookStore.Models.Entities;


namespace BookStore.Common.DataCreate
{
    public static class EntityCreate
    {
        public static void ConfigDbTypes(BookStoreContext bookStoreContext)
        {
            bookStoreContext.Persons.AddRange(
              new Person("Diovana", "Borges Fortes")  
            );

            bookStoreContext.SaveChanges("username1", "sourcename1");
        }
        public static Person SeedDbOnePerson(BookStoreContext bookStoreContext,string firstName, string lastname, string? address, string? gender)
        {
            var person = new Person(firstName, lastname);
            bookStoreContext.Persons.Add(person);
            bookStoreContext.SaveChanges("", "");
            return person;
        }
    }

}