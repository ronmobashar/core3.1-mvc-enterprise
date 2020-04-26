namespace PandasPizzaShop.Models
{
    public class RecipeInformation
    {
        public int RecipeInformationId { get; set; }
        public string PreparationDirections { get; set; }
        public int Duration { get; set; }
        public Pizza Pizza { get; set; }
        public int PizzaId { get; set; }

    }
}
