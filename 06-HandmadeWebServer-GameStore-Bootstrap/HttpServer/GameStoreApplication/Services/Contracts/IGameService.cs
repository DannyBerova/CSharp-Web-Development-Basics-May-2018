namespace HttpServer.GameStoreApplication.Services.Contracts
{
    using System;
    using System.Collections.Generic;

    using ViewModels.Admin;
    using Data.Models;

    public interface IGameService
    {
        bool Create(
            string title,
            string description,
            string image,
            decimal price,
            double size,
            string videoId,
            DateTime releaseDate
            );

        Game FindById(int id);

        IEnumerable<Game> All();

        bool Edit(int id, string title, string description, string image, decimal price, double size, string videoId, DateTime releaseDate);

        bool Delete(int id);

        IEnumerable<Game> FindByUserEmail(string email);

        void AddRange(IEnumerable<UserGame> userGames);
    }
}
