using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PandasPizzaShop.Models
{
    public class PizzaGiftOrder
    {
        [BindNever]
        public int PizzaGiftOrderId { get; set; }
        public Pizza Pizza { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        [StringLength(100)]
        public string Address { get; set; }
    }
}
