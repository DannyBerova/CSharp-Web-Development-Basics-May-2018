using System;
using System.Collections.Generic;
using System.Text;

namespace HttpServer.GameStoreApplication.Data.Seed
{
    using System.Globalization;
    using System.Linq;
    using Models;

    public class Seeder
    {
        public void SeedGamesInGameStore()
        {
            using (var db = new GameStoreDbContext())
            {
                if (!db.Games.Any())
                {
                    Console.WriteLine("Seeding games in database...");
                    var games = new List<Game>
                    {

                        new Game {
                            Title = "Test1",
                            Description = "Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events.",
                            ImageUrl = "http://igg-games.com/wp-content/uploads/2018/03/Need-for-Speed-Payback-Free-Download.jpg",
                            VideoId = "MzADhQLHVt8",
                            Price = 42,
                            Size = 17.8,
                            ReleaseDate = DateTime.ParseExact("2017-10-11", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test2",
                            Description = "Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events. Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events.",
                            ImageUrl = "https://i.ytimg.com/vi/7dHSzsSDnvs/maxresdefault.jpg",
                            VideoId = "MzADhQLHVt8",
                            Price = 59.00m,
                            Size = 17.8,
                            ReleaseDate = DateTime.ParseExact("2016-10-10", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test3",
                            Description = "Witness one of the most popular eSports games of all time, with the objective-based multiplayer first-person shooter Counter-Strike: Global Offensive.Take control of either Terrorists or Counter Terrorists, as you attempt to complete objectives dependent on the team you choose, whilst also trying to eliminate the enemy team. Global Offensive features five online game modes, including Classic Casual and Competitive, Bomb Scenario, Hostage Scenario, Arms Race, Demolition and Deathmatch.",
                            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQENNqsRH-QOmhtkolLom6D01h6Y3q0CzfKd-aEGJIDhCTUdd1c",
                            VideoId = "bCsPUSR7Kzg",
                            Price = 11.99m,
                            Size = 2.7,
                            ReleaseDate = DateTime.ParseExact("2012-05-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test4",
                            Description = "Witness one of the most popular eSports games of all time, with the objective-based multiplayer first-person shooter Counter-Strike: Global Offensive.Take control of either Terrorists or Counter Terrorists, as you attempt to complete objectives dependent on the team you choose, whilst also trying to eliminate the enemy team. Global Offensive features five online game modes, including Classic Casual and Competitive, Bomb Scenario, Hostage Scenario, Arms Race, Demolition and Deathmatch.",
                            ImageUrl = "https://steamcdn-a.akamaihd.net/steam/apps/730/header.jpg?t=1525134820",
                            VideoId = "bCsPUSR7Kzg",
                            Price = 11.99m,
                            Size = 2.7,
                            ReleaseDate = DateTime.ParseExact("2012-05-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test5",
                            Description = "Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events.",
                            ImageUrl = "http://igg-games.com/wp-content/uploads/2018/03/Need-for-Speed-Payback-Free-Download.jpg",
                            VideoId = "MzADhQLHVt8",
                            Price = 23,
                            Size = 12.8,
                            ReleaseDate = DateTime.ParseExact("2017-10-11", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test6",
                            Description = "Witness one of the most popular eSports games of all time, with the objective-based multiplayer first-person shooter Counter-Strike: Global Offensive.Take control of either Terrorists or Counter Terrorists, as you attempt to complete objectives dependent on the team you choose, whilst also trying to eliminate the enemy team. Global Offensive features five online game modes, including Classic Casual and Competitive, Bomb Scenario, Hostage Scenario, Arms Race, Demolition and Deathmatch.",
                            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQENNqsRH-QOmhtkolLom6D01h6Y3q0CzfKd-aEGJIDhCTUdd1c",
                            VideoId = "bCsPUSR7Kzg",
                            Price = 12.92m,
                            Size = 2.8,
                            ReleaseDate = DateTime.ParseExact("2012-05-12", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },
                        new Game {
                            Title = "Test7",
                            Description = "Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events. Enter the Speedcross and beat its current champion, Jason Munroe aka the Barracuda. Two new cars, a brand new car class and sixteen Speedcross events.",
                            ImageUrl = "https://i.ytimg.com/vi/7dHSzsSDnvs/maxresdefault.jpg",
                            VideoId = "MzADhQLHVt8",
                            Price = 59.00m,
                            Size = 17.8,
                            ReleaseDate = DateTime.ParseExact("2015-01-10", "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        },


                    };
                    db.AddRange(games);
                    db.SaveChanges();
                }
            }
        }
    }
}
