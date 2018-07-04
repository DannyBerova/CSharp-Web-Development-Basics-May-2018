using System.Linq.Expressions;

namespace SoftUni.WebServer.Services.Models
{
    using System;
    using System.Globalization;
    using WebServer.Models;

    public class UserAfterCreationModel
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public static Expression<Func<User, UserAfterCreationModel>> FromUser =>
            k => new UserAfterCreationModel()
            {
                Id = k.Id,
                RoleId = k.RoleId,
                RoleName = k.Role.Name
            };
    }
}
