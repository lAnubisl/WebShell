using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace WebShell
{
    public class Execute : IHttpHandler
    {
        private readonly ExecutableService service = new ExecutableService();
        private readonly ConfigurationService config = new ConfigurationService();

        public void ProcessRequest(HttpContext context)
        {
            var executableName = context.Request.QueryString["name"];
            if (string.IsNullOrEmpty(executableName))
            {
                context.Response.StatusCode = 404;
                return;
            }

            var model = service.LoadExecutable(executableName);
            if (model == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            using (var process = new Process())
            {
                process.StartInfo.WorkingDirectory = model.WorkingDirectory;
                process.StartInfo.FileName = model.Path;
                process.StartInfo.Arguments = model.Arguments;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                context.Response.ContentType = "text/event-stream";
                try
                {
                    process.Start();
                    using (var fsw = new StreamWriter(config.GetRunsHistoryPath(executableName, DateTime.Now)))
                    {
                        string line;
                        while ((line = process.StandardOutput.ReadLine()) != null)
                        {
                            fsw.WriteLine(line);
                            context.Response.Write(string.Format("<span>{0}</span><br/>", line));
                            context.Response.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}