using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartCleaningAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        //public Sale()
        //{
        //    Date = DateOnly.FromDateTime(dateTime);
        //    Time = TimeOnly.FromDateTime(dateTime);
        //}

        private DateTime _date;
        public DateTime DateTime
        {
            get 
            {
                return _date;
            }
            set
            {
                _date = value;
                Time = TimeOnly.FromDateTime(_date);
                Date = DateOnly.FromDateTime(_date);
            }
        }

        [JsonIgnore]
        public DateOnly Date { get; private set; }
        [JsonIgnore]
        public TimeOnly Time { get; private set; }

        public int SalesPointId { get; set; }
        public int? BuyerId { get; set; }
        public float TotalAmount { get; set; }
        public ICollection<SaleData>? SalesData { get; set; }

    }
}
