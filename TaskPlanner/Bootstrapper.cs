using System;
using System.IO;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace TaskPlanner
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.OnError += (ctx, ex) =>
            {
                return "Sorry, an error occured";
            };
            base.RequestStartup(container, pipelines, context);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new PathProvider();
            }
        }
    }


    public class PathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            var currentPath =  Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\"));
            return currentPath;
        }
    }
}