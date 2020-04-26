using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PandasPizzaShop.Auth;

namespace PandasPizzaShop.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Pizza>()
                .HasOne(p => p.RecipeInformation)
                .WithOne(i => i.Pizza)
                .HasForeignKey<RecipeInformation>(b => b.PizzaId);
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PizzaReview> PizzaReviews { get; set; }
        public DbSet<PizzaGiftOrder> PizzaGiftOrders { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
