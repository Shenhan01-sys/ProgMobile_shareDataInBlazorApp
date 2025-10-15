namespace PizzaList.Services
{
    public class PizzaSalesState
    {
        private int _pizzasSoldToday = 0;
        private decimal _totalRevenue = 0;
        private List<string> _recentSales = new();

        public int PizzasSoldToday 
        { 
            get => _pizzasSoldToday; 
            set 
            { 
                _pizzasSoldToday = value;
                NotifyStateChanged();
            } 
        }

        public decimal TotalRevenue 
        { 
            get => _totalRevenue; 
            set 
            { 
                _totalRevenue = value;
                NotifyStateChanged();
            } 
        }

        public List<string> RecentSales => _recentSales.ToList();

        public event Action? OnChange;

        public void AddSale(string pizzaName, decimal price)
        {
            _pizzasSoldToday++;
            _totalRevenue += price;
            _recentSales.Insert(0, $"{pizzaName} - {price:C}");
            
            // Keep only last 10 sales
            if (_recentSales.Count > 10)
            {
                _recentSales.RemoveAt(_recentSales.Count - 1);
            }
            
            NotifyStateChanged();
        }

        public void ResetDailySales()
        {
            _pizzasSoldToday = 0;
            _totalRevenue = 0;
            _recentSales.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}