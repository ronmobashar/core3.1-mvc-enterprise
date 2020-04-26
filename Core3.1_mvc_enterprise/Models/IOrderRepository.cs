namespace PandasPizzaShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        void CreatePizzaGiftOrder(PizzaGiftOrder pizzaGiftOrder);
    }
}
