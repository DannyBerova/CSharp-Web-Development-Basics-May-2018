namespace ExamWeb.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;
    using ViewModels;

    public class LogsService
    {
        private readonly ModPanelDbContext db;

        public LogsService()
        {
            this.db = new ModPanelDbContext();
        }

        public void Create(string admin, LogType type, string additionalInformation)
        {
            var log = new Log
            {
                Admin = admin,
                Type = type,
                ActivityInfo = additionalInformation
            };

            this.db.Logs.Add(log);
            this.db.SaveChanges();
        }

        public IEnumerable<LogDetailsModel> All()
            => this.db
                .Logs
                .OrderByDescending(l => l.Id)
                .Select(LogDetailsModel.FromLog)
                .ToList();
    }
}
