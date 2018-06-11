namespace HttpServer.GameStoreApplication.Services
{
    using System;
    using System.Collections.Generic;

    using Data;
    using Data.Models;
    using ViewModels.Admin;
    using Services.Contracts;
    using System.Linq;

    public class GameService : IGameService
    {
        public bool Create(
            string title, 
            string description, 
            string image, 
            decimal price, 
            double size, 
            string videoId, 
            DateTime releaseDate)
        {
            using(var db = new GameStoreDbContext())
            {
                var game = new Game()
                {
                    Title = title,
                    Description = description,
                    ImageUrl = image,
                    Price = price,
                    Size = size,
                    VideoId = videoId,
                    ReleaseDate = releaseDate
                };

                db.Add(game);
                db.SaveChanges();
            }

            return true;
        }

        public Game FindById(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.Find(id);

                return game;
            }
        }

        public IEnumerable<Game> All()
        {
            using (var db = new GameStoreDbContext())
            {
                return db.Games.ToList();
            }
        }
        
        public bool Edit(int id, string title, string description, string image, decimal price, double size, string videoId, DateTime releaseDate)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.Find(id);

                if(game == null)
                {
                    return false;
                }

                game.Title = title;
                game.Description = description;
                game.ImageUrl = image;
                game.Price = price;
                game.Size = size;
                game.VideoId = videoId;
                game.ReleaseDate = releaseDate;

                db.Update(game);
                db.SaveChanges();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var db = new GameStoreDbContext())
            {
                var game = db.Games.Find(id);

                if(game == null)
                {
                    return false;
                }

                db.Games.Remove(game);
                db.SaveChanges();
            }

            return true;
        }

        public IEnumerable<Game> FindByUserEmail(string email)
        {
            using (var db = new GameStoreDbContext())
            {
                return db.UserGames.Where(ug => ug.User.Email == email).Select(ug => ug.Game).ToList();
            }
        }

        public void AddRange(IEnumerable<UserGame> userGames)
        {
            using (var db = new GameStoreDbContext())
            {
                db.AddRange(userGames);
                db.SaveChanges();
            }
        }
    }
}
