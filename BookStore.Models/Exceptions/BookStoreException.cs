namespace BookStore.Models.Exceptions
{
    public enum ErrorNumber
    {
        GetPersonAsync = 1404,
        InvalidId = 2404
    }
    public class Error
    {
        public int Number { get; set; }
        public string? Message { get; set; }
    }
    public  class BookStoreException : Exception
    {
        public Error Error { get; set; }
        public BookStoreException(Error error)
        {
            Error = error;
        }
    }
}