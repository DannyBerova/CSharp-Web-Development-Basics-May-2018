namespace SimpleMVC.Framework.Routers
{
    using System;
    using System.IO;
    using System.Linq;
    using WebServer.Contracts;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;
    using HttpStatusCode = WebServer.Enums.HttpStatusCode;

    public class ResourceRouter : IHandleable
    {
        public IHttpResponse Handle(IHttpRequest request)
        {
            string fileFullName = request.Path.Split("/").Last();
            string fileExtension = request.Path.Split(".").Last();

            IHttpResponse fileResponse = null;

            try
            {
                byte[] fileContent = this.ReadFileContent(fileFullName, fileExtension);

                fileResponse = new FileResponse(HttpStatusCode.Ok, fileContent);
            }
            catch (Exception e)
            {
                return new NotFoundResponse();
            }

            return fileResponse;
        }

        private byte[] ReadFileContent(string fileFullName, string fileExtension)
        {
            byte[] byteContent = File.ReadAllBytes(string.Format(
                "../../../{0}/{1}/{2}",
                MvcContext.Get.ResourcesFolder,
                fileExtension,
                fileFullName));

            return byteContent;
        }
    }
}
