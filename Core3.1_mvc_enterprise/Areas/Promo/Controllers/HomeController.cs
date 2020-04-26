using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PandasPizzaShop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PandasPizzaShop.Areas.Promo.Controllers
{
    [Area("Promo")]
    public class HomeController : Controller
    {
        private List<Pizza> _pizzas;

        public HomeController()
        {
            _pizzas = new List<Pizza> {
                new Pizza
                {
                    Name = "Apple Pizza",
                    Price = 12.95M,
                    PizzaId = 30,
                    ShortDescription = "Our famous apple pizzas!",
                    LongDescription =
                        "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pizza chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pizza muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pizza cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepizza.jpg",
                    InStock = true,
                    IsPizzaOfTheWeek = true,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepizzasmall.jpg",
                    AllergyInformation = ""
                },
                new Pizza
                {
                    Name = "Blueberry Cheese Cake",
                    Price = 18.95M,
                    PizzaId=33,
                    ShortDescription = "You'll love it!",
                    LongDescription =
                        "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pizza chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pizza muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pizza cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
                    InStock = true,
                    IsPizzaOfTheWeek = false,
                    ImageThumbnailUrl =
                        "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg",
                    AllergyInformation = ""
                },
                new Pizza
                {
                    Name = "Cheese Cake",
                    Price = 18.95M,
                    PizzaId = 40,
                    ShortDescription = "Plain cheese cake. Plain pleasure.",
                    LongDescription =
                        "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pizza chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pizza muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pizza cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                    InStock = true,
                    IsPizzaOfTheWeek = false,
                    ImageThumbnailUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
                    AllergyInformation = ""
                }
            };
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_pizzas);
        }
    }
}
