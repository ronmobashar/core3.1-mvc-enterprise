using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PandasPizzaShop.Models
{
    public class PizzaReviewRepository : IPizzaReviewRepository
    {
        private readonly AppDbContext _appDbContext;

        public PizzaReviewRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddPizzaReview(PizzaReview pizzaReview)
        {
            _appDbContext.PizzaReviews.Add(pizzaReview);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<PizzaReview> GetReviewsForPizza(int pizzaId)
        {
            return _appDbContext.PizzaReviews.Where(p => p.Pizza.PizzaId == pizzaId);
        }
    }
}
