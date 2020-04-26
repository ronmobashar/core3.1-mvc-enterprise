using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PandasPizzaShop.Models;

namespace PandasPizzaShop.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20161213151103_AddShoppingCartItem")]
    partial class AddShoppingCartItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PandasPizzaShop.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<string>("Description");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PandasPizzaShop.Models.Pizza", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AllergyInformation");

                    b.Property<int>("CategoryId");

                    b.Property<string>("ImageThumbnailUrl");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("InStock");

                    b.Property<bool>("IsPizzaOfTheWeek");

                    b.Property<string>("LongDescription");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("ShortDescription");

                    b.HasKey("PizzaId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("PandasPizzaShop.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ShoppingCartItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int?>("PizzaId");

                    b.Property<string>("ShoppingCartId");

                    b.HasKey("ShoppingCartItemId");

                    b.HasIndex("PizzaId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("PandasPizzaShop.Models.Pizza", b =>
                {
                    b.HasOne("PandasPizzaShop.Models.Category", "Category")
                        .WithMany("Pizzas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PandasPizzaShop.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("PandasPizzaShop.Models.Pizza", "Pizza")
                        .WithMany()
                        .HasForeignKey("PizzaId");
                });
        }
    }
}
