using System.ComponentModel.DataAnnotations;

namespace PizzaList.Models
{
    public class Pizza
    {
        public const int DefaultSize = 12;
        public const int MinimumSize = 9;
        public const int MaximumSize = 17;

        [Key]
        public int PizzaId { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public string ImageUrl { get; set; } = string.Empty;
        
        [Required]
        public decimal BasePrice { get; set; }

        // Pizza size (for ordering)
        public int Size { get; set; } = DefaultSize;

        // For orders (relationship to special pizza)
        public int? SpecialId { get; set; }
        public Pizza? Special { get; set; }

        public string GetFormattedBasePrice()
        {
            return BasePrice.ToString("C");
        }

        public decimal GetTotalPrice()
        {
            return ((decimal)Size / (decimal)DefaultSize) * BasePrice;
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("C");
        }
    }
}
