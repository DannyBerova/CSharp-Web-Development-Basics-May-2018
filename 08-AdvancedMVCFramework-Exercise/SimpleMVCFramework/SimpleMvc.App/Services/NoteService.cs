namespace SimpleMvc.App.Services
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class NoteService
    {
        private readonly NotesDbContext db;

        public NoteService()
        {
            this.db = new NotesDbContext();
        }

        public void Add(string title, string content, string username)
        {
            var userExist = this.db.Users.Any(u => u.Username == username);
            var user = this.db.Users.FirstOrDefault(u => u.Username == username);

            if (userExist)
            {
                var note = new Note
                {
                    Title = title,
                    Content = content,
                    UserId = user.Id
                };

                this.db.Notes.Add(note);
                this.db.SaveChanges();
            }
        }

        public IEnumerable<Note> AllNotesFromUser(int userId)
        {
            var notes = db.Notes.Where(n => n.UserId == userId).ToList();
            return notes;
        }

        public int GetNotesCountByUserId(int id)
        {
                return db
                    .Notes
                    .Count(u => u.UserId == id);
        }

        public Note GetById(int id)
        {
            var note = this.db.Notes.Find(id);
            return note;
        }

        public void Delete(int id)
        {
            var game = this.db.Notes.Find(id);
            this.db.Notes.Remove(game);

            this.db.SaveChanges();
        }

        public void Update(int id, string modelTitle, string modelContent)
        {
            var note = this.db.Notes.Find(id);

            note.Title = modelTitle;
            note.Content = modelContent;

            this.db.SaveChanges();
        }

        public void ChangeStatus(int id, bool isCheked)
        {
            var note = this.db.Notes.Find(id);
            note.IsFinished = !isCheked;

            this.db.SaveChanges();
        }
    }
}