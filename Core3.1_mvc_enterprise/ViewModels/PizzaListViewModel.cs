using PandasPizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandasPizzaShop.ViewModels
{
    public class PizzaListViewModel
    {
        public IEnumerable<Pizza> Pizzas { get; set; }
        public string CurrentCategory { get; set; }
    }
}
