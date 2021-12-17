﻿using System.Linq;
using System.Threading.Tasks;
using Statiq.App;
using Statiq.Common;
using Statiq.Core;
using Statiq.Handlebars;
using Statiq.Web;
using Statiq.Web.Pipelines;

namespace Website
{
    class Program
    {
        public static async Task<int> Main(string[] args) =>
            await Bootstrapper.Factory
                .CreateWeb(args)
                .ModifyPipeline(nameof(Content), pipeline => pipeline
                    .WithPostProcessModules(
                        new SetMetadata("layout", Config.FromContext(async context => await context.FileSystem
                            .GetInputFile("_Layout.hbs").ReadAllTextAsync())),
                        new RenderHandlebars("layout")
                            .WithModel(Config.FromDocument((input, context) => new {
                                pageTitle = input.GetString("title"),
                                baseUrl = ".",
                                hideTitle = input.GetBool("hide-title", false),
                            }))
                            .WithPartial("textWhatPeopleRead",
                                Config.FromDocument(async input => await input.GetContentStringAsync())),
                        new SetContent(Config.FromDocument(document => document.GetString("layout")))))
                .DeployToNetlify(
                    "bfbed18f-284f-47c4-a365-06b1915d0beb",
                    "t1DXGBr8gP9VzQN62641AbhpN3Fy9j-onVFUdnWLthI")
                .RunAsync();
    }
}
