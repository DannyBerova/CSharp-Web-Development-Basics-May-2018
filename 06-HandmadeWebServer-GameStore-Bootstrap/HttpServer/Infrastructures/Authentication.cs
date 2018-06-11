namespace HttpServer.Infrastructures
{
    public class Authentication
    {
        public Authentication(bool isAuthenticated, bool isAdmin)
        {
            this.IsAthenticated = isAuthenticated;
            this.IsAdmin = isAdmin;
        }

        public bool IsAthenticated { get; private set; }

        public bool IsAdmin { get; private set; }
    }
}
