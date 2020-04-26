using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PandasPizzaShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PandasPizzaShop.ViewModels
{
    public class PizzaEditViewModel
    {
        public Pizza Pizza { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int CategoryId { get; set; }
    }
}
