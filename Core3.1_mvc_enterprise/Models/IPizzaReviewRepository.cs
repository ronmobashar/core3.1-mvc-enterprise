using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandasPizzaShop.Models
{
    public interface IPizzaReviewRepository
    {
        void AddPizzaReview(PizzaReview pizzaReview);
        IEnumerable<PizzaReview> GetReviewsForPizza(int pizzaId);
    }
}
