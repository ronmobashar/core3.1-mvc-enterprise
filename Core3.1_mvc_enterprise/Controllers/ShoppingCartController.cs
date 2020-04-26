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
    public class ShoppingCartController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPizzaRepository pizzaRepository, ShoppingCart shoppingCart)
        {
            _pizzaRepository = pizzaRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int pizzaId)
        {
            var selectedPizza = _pizzaRepository.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

            if (selectedPizza != null)
            {
                _shoppingCart.AddToCart(selectedPizza, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pizzaId)
        {
            var selectedPizza = _pizzaRepository.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

            if (selectedPizza != null)
            {
                _shoppingCart.RemoveFromCart(selectedPizza);
            }
            return RedirectToAction("Index");
        }
    }
}
