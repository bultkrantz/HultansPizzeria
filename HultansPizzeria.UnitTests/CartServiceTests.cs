using HultansPizzeria.Data;
using HultansPizzeria.Models;
using HultansPizzeria.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace HultansPizzeria.UnitTests
{
    public class CartServiceTests
    {
        private readonly IServiceProvider _serviceProvider;
        public CartServiceTests()
        {
            var efServiceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(b =>
            b.UseInMemoryDatabase("Databas")
            .UseInternalServiceProvider(efServiceProvider));
            services.AddTransient<CartService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<RoleManager<ApplicationUser>>();

            _serviceProvider = services.BuildServiceProvider();

        }
        [Fact]
        public void All_Are_Sorted()
        {
            var _ingredients = _serviceProvider.GetService<CartService>();
            var ings = _ingredients.GetIngredients();
            Assert.Equal(ings.Count, 0);
        }
    }
}
