using System.ComponentModel.DataAnnotations;

namespace PizzaList.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();

        public decimal GetTotalPrice()
        {
            return Pizzas.Sum(p => p.GetTotalPrice());
        }

        public string GetFormattedTotalPrice()
        {
            return GetTotalPrice().ToString("C");
        }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public string CustomerPhone { get; set; } = string.Empty;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }

    public enum OrderStatus
    {
        Pending,
        Preparing,
        Ready,
        Delivered,
        Cancelled
    }
}