using PandasPizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandasPizzaShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pizza> PizzasOfTheWeek { get; set; }
    }
}
