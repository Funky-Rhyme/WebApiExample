using System.ComponentModel.DataAnnotations;


namespace SmartCleaningAPI.Models
{
    public class Buyer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<SalesIds>? SalesIds { get; set; }
    }
}
