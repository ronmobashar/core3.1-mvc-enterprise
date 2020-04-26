using System.Collections.Generic;

namespace PandasPizzaShop.Auth
{
    public static class PandasPizzaShopClaimTypes
    {
        public static List<string> ClaimsList { get; set; } = new List<string> { "Delete Pizza", "Add Pizza", "Age for ordering" };
    }
}
