using System.Linq;
using PandasPizzaShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace PandasPizzaShop.Controllers
{
    public class PizzaGiftController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IOrderRepository _orderRepository;

        public PizzaGiftController(IPizzaRepository pizzaRepository, IOrderRepository orderRepository)
        {
            _pizzaRepository = pizzaRepository;
            _orderRepository = orderRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[IgnoreAntiforgeryToken]
        public IActionResult Index(PizzaGiftOrder pizzaGiftOrder)
        {
            var pizzaOfTheMonth = _pizzaRepository.PizzasOfTheWeek.FirstOrDefault();

            if (pizzaOfTheMonth != null)
            {
                pizzaGiftOrder.Pizza = pizzaOfTheMonth;
                _orderRepository.CreatePizzaGiftOrder(pizzaGiftOrder);
                return RedirectToAction("PizzaGiftOrderComplete");
            }

            return View();
        }

        public IActionResult PizzaGiftOrderComplete()
        {
            ViewBag.PizzaGiftOrderCompleteMessage = HttpContext.User.Identity.Name +
                                                  ", thanks for the order. Your friend will soon receive the pizza!";
            return View();
        }
    }
}
