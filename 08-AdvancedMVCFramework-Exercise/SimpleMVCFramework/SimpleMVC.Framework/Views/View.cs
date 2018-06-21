namespace SimpleMVC.Framework.Views
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Interfaces;

    public class View : IRenderable
    {
        public const string BaseLayoutFileName = "Layout";
        public const string ContentPlaceholder = "{{{content}}}";
        public const string HtmlExtension = ".html";
        //public const string LocalPath = "../../../";
        public const string LocalErrorPath = "\\SimpleMvc.Framework\\Errors\\Error.html";

        private readonly string templateFullyQualifiedName;
        private readonly IDictionary<string, string> viewData;

        public View(string templateFullyQualifiedName, IDictionary<string, string> viewData)
        {
            this.templateFullyQualifiedName = templateFullyQualifiedName;
            this.viewData = viewData;
        }

        public string Render()
        {
            string fullHtml = this.ReadFile();

            if (this.viewData.Any())
            {
                foreach (var parameter in this.viewData)
                {
                    fullHtml = fullHtml.Replace($"{{{{{{{parameter.Key}}}}}}}", parameter.Value);
                }
            }

            return fullHtml;
        }

        private string ReadFile()
        {
            string layoutHtml = this.RenderLayoutHtml();

            string templateFullyQualifiedNameWithExtension = this.templateFullyQualifiedName + HtmlExtension;

            if (!File.Exists(templateFullyQualifiedNameWithExtension))
            {
                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);
                this.viewData.Add("error","Requested view does not exist!");

                return errorHtml;
            }

            var templateHtml = File.ReadAllText(templateFullyQualifiedNameWithExtension);

            return layoutHtml.Replace(ContentPlaceholder, templateHtml);
        }

        private string GetErrorPath()
        {
            string appDirectoryPath = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(appDirectoryPath);
            var parentDirectoryPath = parentDirectory.FullName;

            var errorPagePath = parentDirectoryPath + LocalErrorPath;

            return errorPagePath;
        }

        private string RenderLayoutHtml()
        {

            string layotHtmlQualifiedName = string.Format(
                "../../../{0}/{1}{2}",
                MvcContext.Get.ViewsFolder,
                BaseLayoutFileName,
                HtmlExtension
            );
            //string root = Path.GetRelativePath("../../../", layotHtmlQualifiedName);

            if (!File.Exists(layotHtmlQualifiedName))
            {
                string errorPath = this.GetErrorPath();
                string errorHtml = File.ReadAllText(errorPath);
                this.viewData.Add("error", "Layout view does not exist!");

                return errorHtml;
            }

            string layoutHtmlFileContent = File.ReadAllText(layotHtmlQualifiedName);

            return layoutHtmlFileContent;
        }
    }
}
