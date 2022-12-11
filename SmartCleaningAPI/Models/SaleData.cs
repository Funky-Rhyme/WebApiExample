using System.ComponentModel.DataAnnotations;

namespace SmartCleaningAPI.Models
{
    public class SaleData
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductIdAmount { get; set; }
    }
}
