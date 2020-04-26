using System;
using PandasPizzaShop.Filters;
using Microsoft.AspNetCore.Mvc;
using PandasPizzaShop.Models;
using PandasPizzaShop.Utility;
using PandasPizzaShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace PandasPizzaShop.Controllers
{
    //[RequireHeader]
    [ServiceFilter(typeof(TimerAction))]
    //[TimerAction]
    public class HomeController : Controller
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly ILogger<HomeController> _logger;
        private IMemoryCache _memoryCache;

        public HomeController(IPizzaRepository pizzaRepository, IStringLocalizer<HomeController> stringLocalizer, ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _pizzaRepository = pizzaRepository;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        //[ResponseCache(Duration = 30)]
        //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        //[ResponseCache(Duration = 30, VaryByHeader = "User-Agent")]
        [ResponseCache(CacheProfileName = "Default")]
        public ViewResult Index()
        {
            //Serilog
            _logger.LogDebug("Loading home page");
            
            //Application Insights
            //TelemetryClient tc = new TelemetryClient();
            //tc.TrackPageView(new PageViewTelemetry("Insights: Panda's Home page loaded") { Timestamp = DateTime.UtcNow });
            //tc.TrackEvent("HomeControllerLoad");

            //Logic for action method
            //ViewBag.PageTitle = _stringLocalizer["PageTitle"];

            //var homeViewModel = new HomeViewModel
            //{
            //    PizzasOfTheWeek = _pizzaRepository.PizzasOfTheWeek
            //};

            //caching change for IMemoryCache
            List<Pizza> pizzasOfTheWeekCached = null;

            if (!_memoryCache.TryGetValue(CacheEntryConstants.PizzasOfTheWeek, out pizzasOfTheWeekCached))
            {
                pizzasOfTheWeekCached = _pizzaRepository.PizzasOfTheWeek.ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
                cacheEntryOptions.RegisterPostEvictionCallback(FillCacheAgain, this);

                _memoryCache.Set(CacheEntryConstants.PizzasOfTheWeek, pizzasOfTheWeekCached, cacheEntryOptions);
            }

            //pizzasOfTheWeekCached = _memoryCache.GetOrCreate(CacheEntryConstants.PizzasOfTheWeek, entry =>
            //{
            //    entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            //    entry.Priority = CacheItemPriority.High;
            //    return _pizzaRepository.PizzasOfTheWeek.ToList();
            //});

            var homeViewModel = new HomeViewModel
            {
                PizzasOfTheWeek = pizzasOfTheWeekCached
            };

            return View(homeViewModel);
        }

        private void FillCacheAgain(object key, object value, EvictionReason reason, object state)
        {
            _logger.LogInformation(LogEventIds.LoadHomepage, "Cache was cleared: reason " + reason.ToString());
        }

        public IActionResult TestUrl()
        {
            // Generates /Pizza/Details/1		
            var url =
                Url.Action("Details", "Pizza", new { id = 1 });
            //return Content(url);
            return RedirectToAction("Index");
        }

        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //Logging
            _logger.LogInformation(LogEventIds.ChangeLanguage, "Language changed to {0}", culture);

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
    }
}