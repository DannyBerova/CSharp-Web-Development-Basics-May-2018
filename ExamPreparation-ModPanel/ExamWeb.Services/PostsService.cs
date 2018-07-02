namespace ExamWeb.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ViewModels;

    public class PostsService
    {
        private readonly ModPanelDbContext db;

        public PostsService()
        {
            this.db = new ModPanelDbContext();
        }

        public bool Create(string title, string content, int userId)
        {
            try
            {
                var post = new Post
                {
                    Title = title.CapitalizeFirstLetter(),
                    Content = content,
                    UserId = userId,
                    CreatedOn = DateTime.UtcNow
                };

                this.db.Posts.Add(post);
                this.db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<PostListingModels> All()
            => this.db
                .Posts
                .Select(PostListingModels.FromPost)
                .ToList();

        public IEnumerable<HomeListingModels> AllWithDetails(string search = null)
        {
            var query = this.db.Posts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Title.ToLower().Contains(search.ToLower()));
            }

            return query
                .OrderByDescending(p => p.Id)
                .Include(p => p.User)
                .Select(HomeListingModels.FromPost)
                .ToList();
        }

        public PostModel GetById(int id)
            => this.db
                .Posts
                .Where(p => p.Id == id)
                .Select(PostModel.FromPost)
                .FirstOrDefault();

        public void Update(int id, string title, string content)
        {
            var post = this.db.Posts.Find(id);

            if (post == null)
            {
                return;
            }

            post.Title = title.CapitalizeFirstLetter();
            post.Content = content;

            this.db.SaveChanges();
        }

        public string Delete(int id)
        {
            var post = this.db.Posts.Find(id);

            if (post == null)
            {
                return null;
            }

            this.db.Posts.Remove(post);
            this.db.SaveChanges();

            return post.Title;
        }
    }
}

