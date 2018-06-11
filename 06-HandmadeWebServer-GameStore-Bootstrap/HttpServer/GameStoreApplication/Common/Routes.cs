namespace HttpServer.GameStoreApplication.Common
{
    public class Routes
    {
        public const string HomePath = "/";
        public const string LoginPath = "/account/login";
        public const string LogoutPath = "/account/logout";
        public const string RegisterPath = "/account/register";
        public const string CartPath = "/account/cart";
        public const string CartRemoveItemPath = "/account/cart/remove/{(?<id>[0-9]+)}";
        public const string CartRemoveItemRegexPath = "/account/cart/remove/(?<id>[0-9]+)";
        public const string AddGamePath = "/admin/games/add";
        public const string ListAllGamesPath = "/admin/games/list";
        public const string EditGamePath = "/admin/games/edit/{(?<id>[0-9]+)}";
        public const string DeleteGamePath = "/admin/games/delete/{(?<id>[0-9]+)}";
        public const string GameDetailsPath = "/games/details/{(?<id>[0-9]+)}";
        public const string GameDetailsRegexPath = "/games/details/(?<id>[0-9]+)";
        public const string BuyGamePath = "/games/buy/{(?<id>[0-9]+)}";
        public const string BuyGameRegexPath = "/games/buy/(?<id>[0-9]+)";
    }
}
