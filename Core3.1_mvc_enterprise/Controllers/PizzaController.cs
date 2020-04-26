using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using PandasPizzaShop.Filters;
using Microsoft.AspNetCore.Mvc;
using PandasPizzaShop.Models;
using PandasPizzaShop.Utility;
using PandasPizzaShop.ViewModels;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PandasPizzaShop.Controllers
{
    [PizzaNotFoundException]
    public class PizzaController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPizzaReviewRepository _pizzaReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(IPizzaRepository pizzaRepository, ICategoryRepository categoryRepository, ILogger<PizzaController> logger,
            IPizzaReviewRepository pizzaReviewRepository, HtmlEncoder htmlEncoder)
        {
            _pizzaRepository = pizzaRepository;
            _categoryRepository = categoryRepository;
            _pizzaReviewRepository = pizzaReviewRepository;
            _htmlEncoder = htmlEncoder;
            _logger = logger;
        }

        //[Route("AllPizzas")]
        //[Route("ListOfPizzas")]
        public ViewResult List(string category)
        {
            IEnumerable<Pizza> pizzas;
            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                pizzas = _pizzaRepository.Pizzas.OrderBy(p => p.PizzaId);
                currentCategory = "All pizzas";
            }
            else
            {
                pizzas = _pizzaRepository.Pizzas.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.PizzaId);
                currentCategory = _categoryRepository.Categories.FirstOrDefault(c => c.CategoryName == category).CategoryName;
            }

            return View(new PizzaListViewModel
            {
                Pizzas = pizzas,
                CurrentCategory = currentCategory
            });
        }

        [Route("[controller]/Details/{id}")]
        public IActionResult Details(int id)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogDebug(LogEventIds.GetPizzaIdNotFound, new Exception("Pizza not found"), "Pizza with id {0} not found", id);
                //return NotFound();
                //Catch this error using the exception filter
                throw new PizzaNotFoundException();
            }

            return View(new PizzaDetailViewModel() { Pizza = pizza });
        }

        [Route("[controller]/Details/{id}")]
        [HttpPost]
        public IActionResult Details(int id, string review)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogWarning(LogEventIds.GetPizzaIdNotFound, new Exception("Pizza not found"), "Pizza with id {0} not found", id);
                return NotFound();
            }

            string encodedReview = _htmlEncoder.Encode(review);

            _pizzaReviewRepository.AddPizzaReview(new PizzaReview() { Pizza = pizza, Review = encodedReview });

            return View(new PizzaDetailViewModel() { Pizza = pizza });
        }

    }
}
