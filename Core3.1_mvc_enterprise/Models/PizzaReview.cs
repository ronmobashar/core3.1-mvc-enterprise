namespace PandasPizzaShop.Models
{
    public class PizzaReview
    {
        public int PizzaReviewId { get; set; }
        public Pizza Pizza { get; set; }
        public string Review { get; set; }
    }
}
