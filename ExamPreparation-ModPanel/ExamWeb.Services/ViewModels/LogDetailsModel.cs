namespace ExamWeb.Services.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using ExamWeb.Models;

    public class LogDetailsModel
    {
        public string Admin { get; set; }

        public LogType Type { get; set; }

        public string ActivityInfo { get; set; }

        public static Expression<Func<Log, LogDetailsModel>> FromLog =>
            k => new LogDetailsModel()
            {
                Admin = k.Admin,
                Type = k.Type,
                ActivityInfo = k.ActivityInfo
            };

        public override string ToString()
        {
            string message = null;

            switch (this.Type)
            {
                case LogType.CreatePost:
                    message = $"created the post {this.ActivityInfo}";
                    break;
                case LogType.EditPost:
                    message = $"edited the post {this.ActivityInfo}";
                    break;
                case LogType.DeletePost:
                    message = $"deleted the post {this.ActivityInfo}";
                    break;
                case LogType.UserApproval:
                    message = $"approved the registration of {this.ActivityInfo}";
                    break;
                case LogType.OpenMenu:
                    message = $"opened {this.ActivityInfo} menu";
                    break;
                default:
                    throw new InvalidOperationException($"Invalid log type: {this.Type}.");
            }

            return $"{this.Admin} {message}";
        }
    }
}
