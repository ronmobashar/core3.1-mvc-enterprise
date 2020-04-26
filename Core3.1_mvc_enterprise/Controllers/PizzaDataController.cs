using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PandasPizzaShop.Models;
using PandasPizzaShop.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PandasPizzaShop.Controllers
{
    [Route("api/[controller]")]
    public class PizzaDataController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaDataController(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        [HttpGet]
        public IEnumerable<PizzaViewModel> LoadMorePizzas()
        {
            IEnumerable<Pizza> dbPizzas = null;

            dbPizzas = _pizzaRepository.Pizzas.OrderBy(p => p.PizzaId).Take(10);

            List<PizzaViewModel> pizzas = new List<PizzaViewModel>();

            foreach (var dbPizza in dbPizzas)
            {
                pizzas.Add(MapDbPizzaToPizzaViewModel(dbPizza));
            }
            return pizzas;
        }

        private PizzaViewModel MapDbPizzaToPizzaViewModel(Pizza dbPizza)
        {
            return new PizzaViewModel()
            {
                PizzaId = dbPizza.PizzaId,
                Name = dbPizza.Name,
                Price = dbPizza.Price,
                ShortDescription = dbPizza.ShortDescription,
                ImageThumbnailUrl = dbPizza.ImageThumbnailUrl
            };
        }
    }
}
