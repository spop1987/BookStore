namespace BookStore.Models.Dtos
{
    public class PersonDto
    {
        public long PersonId {get; set;}
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ActivityLogDto? ActivityLog {get; set;}
    }
}