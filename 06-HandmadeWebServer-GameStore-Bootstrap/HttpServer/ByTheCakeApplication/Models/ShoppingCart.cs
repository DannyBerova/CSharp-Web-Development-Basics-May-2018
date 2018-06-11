namespace HttpServer.ByTheCakeApplication.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public const string SessionKey = "^%Current_Shoping_Cart%^";

        public List<Product> Orders { get; private set; } = new List<Product>();
    }
}
