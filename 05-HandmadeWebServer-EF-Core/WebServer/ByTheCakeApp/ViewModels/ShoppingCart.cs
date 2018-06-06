namespace MyWebServer.ByTheCakeApp.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ShoppingCart
    {
        public const string SessionKey = "^%Current_Shopping_Cart%^";

        public HashSet<int> ProductIds { get; private set; } = new HashSet<int>();
    }
}
