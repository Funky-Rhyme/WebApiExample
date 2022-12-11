using System.ComponentModel.DataAnnotations;

namespace SmartCleaningAPI.Models
{
    public class SalesPoint
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<ProvidedProduct>? ProvidedProducts { get; set; }
    }
}
