namespace BookStore.Models.Entities
{
    public class EntityLogBase
    {
        public string? CreateBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedSystem { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedSystem { get; set; }
    }
}