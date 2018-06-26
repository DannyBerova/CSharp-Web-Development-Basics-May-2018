namespace KittensApp.WebIt.Services
{
    using System.Linq;
    using KittenApp.Data;
    using KittenApp.Models;
    using SimpleMvc.Common;

    public class KittensService
    {
        private readonly KittenAppContext db;

        public KittensService()
        {
            this.db = new KittenAppContext();
        }
        public Kitten Create(string name, int age, Breed breed)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            try
            {
                var kitten = new Kitten()
                {
                    Name = name,
                    Age = age,
                    Breed = breed
                };

                this.db.Kittens.Add(kitten);
                this.db.SaveChanges();

                return kitten;
            }
            catch
            {
                return null;
            }
        }

        public Breed GetBreed(string modelBreed)
        {
           return this.db.Breeds
                .FirstOrDefault
                    (b => b.Name == modelBreed);
        }
    }
}
