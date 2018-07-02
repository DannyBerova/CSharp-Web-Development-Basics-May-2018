using System;

namespace ExamWeb.App.Infrastructure
{
    using Services.ViewModels;

    public static class HtmlHelpers
    {
        public static string ToHtml(this LogDetailsModel log)
            => $@"
                <div class=""card border-{log.Type.ToViewClassName()} mb-1"">
                    <div class=""card-body"">
                        <p class=""card-text"">{log}</p>
                    </div>
                </div>";

        public static string ToHtml(this PostListingModels post)
            => $@"
                    <tr>
                        <td>{post.Id}</td>
                        <td>{post.Title}</td>
                        <td>
                            <a class=""btn btn-warning btn-sm"" href=""/admin/edit?id={post.Id}"">Edit</a>
                            <a class=""btn btn-danger btn-sm"" href=""/admin/delete?id={post.Id}"">Delete</a>
                        </td>
                    </tr>";

        public static string ToHtml(this UserWithDetailsModel user)
            => $@"
                    <tr>
                        <td>{user.Id}</td>
                        <td>{user.Email}</td>
                        <td>{user.Position}</td>
                        <td>{user.Posts}</td>
                        <td>
                            {(user.IsApproved ? string.Empty : $@"<a class=""btn btn-primary btn-sm"" href=""/admin/approve?id={user.Id}"">Approve</a>")}
                        </td>
                    </tr>";

        public static string ToHtml(this HomeListingModels post)
            => $@"
                            <div class=""card border-primary mb-3"">
                                <div class=""card-body text-primary"">
                                    <h4 class=""card-title"">{post.Title}</h4>
                                    <p class=""card-text"">
                                        {post.Content}
                                    </p>
                                </div>
                                <div class=""card-footer bg-transparent text-right"">
                                    <span class=""text-muted"">
                                        Created on {(post.CreatedOn ?? DateTime.UtcNow).ToShortDateString()} by
                                        <em>
                                            <strong>{post.CreatedBy}</strong>
                                        </em>
                                    </span>
                                </div>
                            </div>";
    }
}
