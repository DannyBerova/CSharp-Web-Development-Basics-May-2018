namespace SimpleMvc.AppMeTube.Helpers
{
    using Services.ViewModels;

    public static class HtmlHelpers
    {
        public static string ToHtml(this TubeViewModel tube)
            => $@"
                <div class=""col-lg-4 col-md-12 col-sm-12 py-2"">
                    <a href=""/tubes/details?id={tube.Id}"" target=""_self""> 
                    <img class=""card-image-top img-fluid img-thumbnail"" width=""400"" 
                    src=""https://img.youtube.com/vi/{tube.VideoId}/0.jpg"" border=""0"" />
                    </a>
                   
                    <div class=""card-body"">
                        <h3 class=""card-title text-center""><strong>{tube.Title.Shortify(30)}</strong></h3>
                        <h4 class=""card-text text-center""><em>{tube.Author}</em></h4>
                       </br>
                    </div>
                </div>";

        public static string ToHtml(this TubeShortViewModel tube)
            => $@"
                <tr>
                    <td>#</td>
                    <td><small>{tube.Id}</small></td>
                    <td><small>{tube.Title}</small></td>
                    <td><small>{tube.Author}</small></td>
                    <td>
                        <a href=""/tubes/details?id={tube.Id}"">Details</a>
                    </td>
                </tr>";
    }
}
