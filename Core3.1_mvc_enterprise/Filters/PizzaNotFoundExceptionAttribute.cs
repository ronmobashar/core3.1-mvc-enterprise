using PandasPizzaShop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PandasPizzaShop.Filters
{
    public class PizzaNotFoundExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is PizzaNotFoundException)
            {
                context.Result = new ViewResult
                {
                    ViewName = "PizzaNotFound",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = "An error occured while searching the requested pizza"
                    }
                };
            }
        }
    }
}
