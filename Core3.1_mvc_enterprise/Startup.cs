using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PandasPizzaShop.Auth;
using PandasPizzaShop.Filters;
using PandasPizzaShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PandasPizzaShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            })
                 .AddEntityFrameworkStores<AppDbContext>();

       //     services.AddAuthentication()
       //.AddGoogle(options =>
       //{
       //    IConfigurationSection googleAuthNSection =
       //        Configuration.GetSection("Authentication:Google");

       //    options.ClientId = googleAuthNSection["ClientId"];
       //    options.ClientSecret = googleAuthNSection["ClientSecret"];
       //});

            services.AddTransient<IPizzaRepository, PizzaRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPizzaReviewRepository, PizzaReviewRepository>();

            services.AddAntiforgery();
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddMvc
                 (
                     config =>
                     {
                         config.Filters.AddService(typeof(TimerAction));
                         config.CacheProfiles.Add("Default",
                             new CacheProfile()
                             {
                                 Duration = 30,
                                 Location = ResponseCacheLocation.Any
                             });
                         config.CacheProfiles.Add("None",
                             new CacheProfile()
                             {
                                 Location = ResponseCacheLocation.None,
                                 NoStore = true
                             });
                     }
                 )
                 .AddViewLocalization(
                     LanguageViewLocationExpanderFormat.Suffix,
                     opts => { opts.ResourcesPath = "Resources"; })
                 .AddDataAnnotationsLocalization();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/jpeg" });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("DeletePizza", policy => policy.RequireClaim("Delete Pizza", "Delete Pizza"));
                options.AddPolicy("AddPizza", policy => policy.RequireClaim("Add Pizza", "Add Pizza"));
                options.AddPolicy("MinimumOrderAge", policy => policy.Requirements.Add(new MinimumOrderAgeRequirement(18)));
            });

            services.AddMemoryCache();
            services.AddSession();

            //Filters
            services.AddScoped<TimerAction>();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddControllersWithViews();//services.AddMvc(); would also work still
            services.AddRazorPages();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/AppException");
            }
            //gzip compression
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseIdentity();
            loggerFactory.CreateLogger("Debug");


            //app.UseGoogleAuthentication(new GoogleOptions
            //{
            //    ClientId = "351865068992-9n45989ahb0ot26m10clusj206a7ub4n.apps.googleusercontent.com",
            //    ClientSecret = "ERD5Lvjf8TpGUUnuyTGv27vo"
            //});
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>
            {
                //areas
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                name: "categoryfilter",
                pattern: "Pizza/{action}/{category?}",
                defaults: new { Controller = "Pizza", action = "List" });

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

        }
    }
}
