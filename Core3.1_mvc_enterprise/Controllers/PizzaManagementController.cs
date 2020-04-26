using System.Collections.Generic;
using System.Linq;
using PandasPizzaShop.Models;
using PandasPizzaShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PandasPizzaShop.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Authorize(Policy = "DeletePizza")]
    public class PizzaManagementController: Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PizzaManagementController(IPizzaRepository pizzaRepository, ICategoryRepository categoryRepository)
        {
            _pizzaRepository = pizzaRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult Index()
        {
            var pizzas = _pizzaRepository.Pizzas.OrderBy(p => p.PizzaId);
            return View(pizzas);
        }

        public IActionResult AddPizza()
        {
            var categories = _categoryRepository.Categories;
            var pizzaEditViewModel = new PizzaEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                CategoryId = categories.FirstOrDefault().CategoryId
            };
            return View(pizzaEditViewModel);
        }

        [HttpPost]
        public IActionResult AddPizza(PizzaEditViewModel pizzaEditViewModel)
        {
            //Basic validation
            //if (ModelState.IsValid)
            //{
            //    _pizzaRepository.CreatePizza(pizzaEditViewModel.Pizza);
            //    return RedirectToAction("Index");
            //}

            //custom validation rules
            if (ModelState.GetValidationState("Pizza.Price") == ModelValidationState.Valid
                || pizzaEditViewModel.Pizza.Price < 0)
                ModelState.AddModelError(nameof(pizzaEditViewModel.Pizza.Price), "The price of the pizza should be higher than 0");

            if (pizzaEditViewModel.Pizza.IsPizzaOfTheWeek && !pizzaEditViewModel.Pizza.InStock)
                ModelState.AddModelError(nameof(pizzaEditViewModel.Pizza.IsPizzaOfTheWeek), "Only pizzas that are in stock should be Pizza of the Week");

            if (ModelState.IsValid)
            {
                _pizzaRepository.CreatePizza(pizzaEditViewModel.Pizza);
                return RedirectToAction("Index");
            }

            return View(pizzaEditViewModel);
        }

        //public IActionResult EditPizza([FromRoute]int pizzaId)
        //public IActionResult EditPizza([FromQuery]int pizzaId, [FromHeader] string accept)
        public IActionResult EditPizza([FromQuery]int pizzaId, [FromHeader(Name = "Accept-Language")] string accept)
        {
            var categories = _categoryRepository.Categories;

            var pizza = _pizzaRepository.Pizzas.FirstOrDefault(p => p.PizzaId == pizzaId);

            var pizzaEditViewModel = new PizzaEditViewModel
            {
                Categories = categories.Select(c => new SelectListItem() { Text = c.CategoryName, Value = c.CategoryId.ToString() }).ToList(),
                Pizza = pizza,
                CategoryId = pizza.CategoryId
            };

            var item = pizzaEditViewModel.Categories.FirstOrDefault(c => c.Value == pizza.CategoryId.ToString());
            item.Selected = true;

            return View(pizzaEditViewModel);
        }

        [HttpPost]
        //public IActionResult EditPizza([Bind("Pizza")] PizzaEditViewModel pizzaEditViewModel)
        public IActionResult EditPizza(PizzaEditViewModel pizzaEditViewModel)
        {
            pizzaEditViewModel.Pizza.CategoryId = pizzaEditViewModel.CategoryId;

            if (ModelState.IsValid)
            {
                _pizzaRepository.UpdatePizza(pizzaEditViewModel.Pizza);
                return RedirectToAction("Index");
            }
            return View(pizzaEditViewModel);
        }

        [HttpPost]
        public IActionResult DeletePizza(string pizzaId)
        {
            return View();
        }

        public IActionResult QuickEdit()
        {
            var pizzaNames = _pizzaRepository.Pizzas.Select(p => p.Name).ToList();
            return View(pizzaNames);
        }

        [HttpPost]
        public IActionResult QuickEdit(List<string> pizzaNames)
        {
            //Do awesome things with the pizza names here
            return View();
        }

        public IActionResult BulkEditPizzas()
        {
            var pizzaNames = _pizzaRepository.Pizzas.ToList();
            return View(pizzaNames);
        }

        [HttpPost]
        public IActionResult BulkEditPizzas(List<Pizza> pizzas)
        {
            //Do awesome things with the pizza here
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckIfPizzaNameAlreadyExists([Bind(Prefix = "Pizza.Name")]string name)
        {
            var pizza = _pizzaRepository.Pizzas.FirstOrDefault(p => p.Name == name);
            return pizza == null ? Json(true) : Json("That pizza name is already taken");
        }
    }
}