using PizzaList.Models;

namespace PizzaList.Services
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }
        public Pizza? ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();

        public event Action? OnChange;

        public void ShowConfigurePizzaDialog(Pizza special)
        {
            ConfiguringPizza = new Pizza()
            {
                Name = special.Name,
                Description = special.Description,
                BasePrice = special.BasePrice,
                ImageUrl = special.ImageUrl,
                Special = special,
                SpecialId = special.PizzaId,
                Size = Pizza.DefaultSize
            };

            ShowingConfigureDialog = true;
            NotifyStateChanged();
        }

        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
            NotifyStateChanged();
        }

        public void ConfirmConfigurePizzaDialog()
        {
            if (ConfiguringPizza != null)
            {
                Order.Pizzas.Add(ConfiguringPizza);
                ConfiguringPizza = null;
            }

            ShowingConfigureDialog = false;
            NotifyStateChanged();
        }

        public void UpdatePizzaSize(int newSize)
        {
            if (ConfiguringPizza != null)
            {
                ConfiguringPizza.Size = newSize;
                NotifyStateChanged();
            }
        }

        public void ClearOrder()
        {
            Order = new Order();
            NotifyStateChanged();
        }

        public void RemovePizzaFromOrder(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}