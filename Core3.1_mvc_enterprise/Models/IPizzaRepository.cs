using System.Collections.Generic;

namespace PandasPizzaShop.Models
{
    public interface IPizzaRepository
    {
        IEnumerable<Pizza> Pizzas { get; }
        IEnumerable<Pizza> PizzasOfTheWeek { get; }

        Pizza GetPizzaById(int pizzaId);

        void CreatePizza(Pizza pizza);

        void UpdatePizza(Pizza pizza);
    }
}
