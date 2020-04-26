using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PandasPizzaShop.Utility;
using Microsoft.AspNetCore.Mvc;

namespace PandasPizzaShop.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }

        [Remote("CheckIfPizzaNameAlreadyExists", "PizzaManagement", ErrorMessage = "That name already exists")]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AllergyInformation { get; set; }
        public decimal Price { get; set; }

        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageUrl { get; set; }

        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageThumbnailUrl { get; set; }
        public bool IsPizzaOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<PizzaReview> PizzaReviews { get; set; }

        //Specific for Model Binding
        public SugarLevel SugarLevel { get; set; }
        public RecipeInformation RecipeInformation { get; set; }


    }
}
