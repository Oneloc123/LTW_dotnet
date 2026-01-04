using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public static class CartSessionHelper
    {
        private const string CART_KEY = "CART";

        public static Carts GetCart(HttpContext httpContext)
        {
            var session = httpContext.Session;
            var json = session.GetString(CART_KEY);
            if (string.IsNullOrEmpty(json))
            {
                var cart = new Carts();
                SaveCart(httpContext, cart);
                return cart;
            }
            return JsonSerializer.Deserialize<Carts>(json);
        }

        public static void SaveCart(HttpContext httpContext, Carts cart)
        {
            httpContext.Session.SetString(CART_KEY, JsonSerializer.Serialize(cart));
        }

        public static void Clear(HttpContext httpContext)
        {
            httpContext.Session.Remove(CART_KEY);
        }
    }
}
