namespace SoftUni.ChushkaApp.Infrastructure
{
    using WebServer.Services.Models;

    public static class HtmlHelpers
    {
        public static string ToHtml(this ProductsListingModel product)
            => $@"

                <a href=""/products/details?id={product.Id}"" class=""col-md-2 m-2 "" style=""height: 250px;"" >
                    <div class=""product p-1 chushka-bg-color rounded-top rounded-bottom"">
                        <h5 class=""text-center mt-3"" style=""height: 60px;"">{product.Name}</h5>
                        <hr class=""hr-1 bg-white""/>
                        <p class=""text-white text-center justify-content-between""  style=""height: 70px;"">
                            {product.Description.Shortify(50)}
                        </p>
                        <hr class=""hr-1 bg-white""/>
                        <h6 class=""text-center text-white mb-3"">${product.Price}</h6>
                    </div>              
                </a>

";


        public static string ToHtml(this OrdersListingModel order)
            => $@"
                    <tr>
                        <td>{order.Id}</td>
                        <td>{order.Customer}</td>
                        <td>{order.Product}</td>
                        <td>{order.SalePrice}</td>
                        <td>{order.OrderedOn}</td>
                    </tr>";

        //public static string ToHtml(this HomeListingModels post)
        //    => $@"
        //                    <div class=""card border-primary mb-3"">
        //                        <div class=""card-body text-primary"">
        //                            <h4 class=""card-title"">{post.Title}</h4>
        //                            <p class=""card-text"">
        //                                {post.Content}
        //                            </p>
        //                        </div>
        //                        <div class=""card-footer bg-transparent text-right"">
        //                            <span class=""text-muted"">
        //                                Created on {(post.CreatedOn ?? DateTime.UtcNow).ToShortDateString()} by
        //                                <em>
        //                                    <strong>{post.CreatedBy}</strong>
        //                                </em>
        //                            </span>
        //                        </div>
        //                    </div>";
    }
}
