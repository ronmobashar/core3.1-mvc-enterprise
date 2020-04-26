using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PandasPizzaShop.Models
{
    public class PizzaRepository: IPizzaRepository
    {
        private readonly AppDbContext _appDbContext;

        public PizzaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pizza> Pizzas
        {
            get
            {
                return _appDbContext.Pizzas.Include(c => c.Category);
            }
        }

        public IEnumerable<Pizza> PizzasOfTheWeek
        {
            get
            {
                return _appDbContext.Pizzas.Include(c => c.Category).Where(p => p.IsPizzaOfTheWeek);
            }
        }

        public Pizza GetPizzaById(int pizzaId)
        {
            return _appDbContext.Pizzas.Include(p => p.PizzaReviews).FirstOrDefault(p => p.PizzaId == pizzaId);
        }

        public void UpdatePizza(Pizza pizza)
        {
            _appDbContext.Pizzas.Update(pizza);
            _appDbContext.SaveChanges();
        }

        public void CreatePizza(Pizza pizza)
        {
            _appDbContext.Pizzas.Add(pizza);
            _appDbContext.SaveChanges();
        }
    }
}
