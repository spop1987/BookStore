namespace BookStore.Models.Dtos
{
    public class ActivityLogDto
    {
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string? createdSystem { get; set; }
        public string? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? updatedSystem { get; set; }
    }
}