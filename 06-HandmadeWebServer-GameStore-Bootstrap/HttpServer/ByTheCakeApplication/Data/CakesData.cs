namespace HttpServer.ByTheCakeApplication.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.IO;

    using Models;

    public class CakesData
    {
        private const string DatabaseFile = @"../../../ByTheCakeApplication\Data\Database.csv";

        public void Add(string name, string price)
        {
            var streamReader = new StreamReader(DatabaseFile);

            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;

            streamReader.Dispose();

            using (var streamWriter = new StreamWriter(DatabaseFile, true))
            {
                streamWriter.WriteLine($"{id},{name},{price}");
            }
        }

        public IEnumerable<Product> All()
        {
            return File
                    .ReadAllLines(@"../../../ByTheCakeApplication\Data\Database.csv")
                    .Select(l => l.Split(','))
                    .Select(l => new Product()
                    {
                        Id = int.Parse(l[0]),
                        Name = l[1],
                        Price = decimal.Parse(l[2])
                    });
        }

        public Product Find(int id)
            => this.All().FirstOrDefault(c => c.Id == id);        
    }
}
