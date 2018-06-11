namespace HttpServer.Server.Http.Response
{
    using System;
    using System.IO;
    using Enums;

    public class FileResponse : HttpResponse
    {
        public FileResponse(string path)
        {
            this.StatusCode = HttpStatusCode.Ok;
            
            using(var streamReader = new StreamReader(@"../../../GameStoreApplication" + path))
            {
                this.Content = streamReader.ReadToEnd();
            }
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + this.Content;
        }
    }
}
