namespace ExamWeb.App.Infrastructure
{
    using ExamWeb.Models;
    using System;

    public static class EnumsExtension
    {

        public static string ToViewClassName(this LogType type)
        {
            switch (type)
            {
                case LogType.CreatePost:
                    return "success";
                case LogType.EditPost:
                    return "warning";
                case LogType.DeletePost:
                    return "danger";
                case LogType.UserApproval:
                    return "success";
                case LogType.OpenMenu:
                    return "primary";
                default:
                    throw new InvalidOperationException($"Invalid log type {type}.");
            }
        }
    }
}
