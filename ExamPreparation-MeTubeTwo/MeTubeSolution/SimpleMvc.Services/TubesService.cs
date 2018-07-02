using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvc.Services
{
    using System.Linq;
    using Data;
    using Modelss;
    using ViewModels;

    public class TubesService
    {
        private readonly MeTubeDbContext db;

        public TubesService()
        {
            this.db = new MeTubeDbContext();
        }

        public int Add(
            string title,
            string author,
            string description,
            string videoId,
            string username)
        {
            var userExist = this.db.Users.Any(u => u.Username == username);
            var user = this.db.Users.FirstOrDefault(u => u.Username == username);

            if (userExist)
            {
                var tube = new Tube
                {
                    Title = title,
                    Author = author,
                    Description = description,
                    VideoId = videoId,
                    UploaderId = user.Id
                };

                this.db.Tubes.Add(tube);
                this.db.SaveChanges();
                return tube.Id;
            }

            return default(int);
        }

        public IEnumerable<TubeViewModel> All()
        {
            var tubes = this.db.Tubes
                .Select(t => new TubeViewModel()
                {
                    Id = t.Id,
                    Author = t.Author,
                    Title = t.Title,
                    VideoId = t.VideoId
                });
            return tubes;
        }

        public IEnumerable<TubeShortViewModel> AllByUser(int id)
        {
            var tubes = this.db.Tubes
                .Where(t => t.UploaderId == id)
                .Select(t => new TubeShortViewModel()
                {
                    Id = t.Id,
                    Author = t.Author,
                    Title = t.Title,
                });
            return tubes;
        }

        public TubeDetailsViewModel GetById(int id)
        {
            var tube = this.db.Tubes.Where(t => t.Id == id).Select(t => new TubeDetailsViewModel()
            {
                Title = t.Title,
                Author = t.Author,
                Description = t.Description,
                VideoId = t.VideoId,
                UploaderId = t.UploaderId,
                Views = t.Views
            })
                .FirstOrDefault();

            return tube;
        }

        public void Delete(int id)
        {
            var game = this.db.Tubes.Find(id);
            this.db.Tubes.Remove(game);

            this.db.SaveChanges();
        }

        public void IncrementViewsByOne(int id)
        {
            var tube = this.db.Tubes.Find(id);

            tube.Views += 1;
            //this.db.Update(tube);
            this.db.SaveChanges();
        }
    }
}
