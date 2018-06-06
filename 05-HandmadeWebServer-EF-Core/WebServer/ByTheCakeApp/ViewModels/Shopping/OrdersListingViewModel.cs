namespace MyWebServer.ByTheCakeApp.ViewModels.Shopping
{
    using System;

    public class OrdersListingViewModel
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public decimal TotalSum { get; set; }

    }
}
