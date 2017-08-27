using HultansPizzeria.Models;
using HultansPizzeria.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HultansPizzeria.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ICartService _cartService;
        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            Cart cart = _cartService.GetCart() ?? new Cart();
            return View(cart);
        }
    }
}
