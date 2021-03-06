﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PeachPied.WordPress.Sdk.Internal
{
    sealed class PeachpieWpPlugin : IWpPlugin
    {
        public static readonly string InformationalVersion = typeof(PeachpieWpPlugin).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        static string GetDashboardRightNow()
        {
            return string.Format(
                "<ul>" +
                "<li><img src='{0}' style='width:76px;margin:0 auto;display:block;'/></li>" +
                "<li>{1}</li>" +
                "</ul>",
                "https://raw.githubusercontent.com/peachpiecompiler/peachpie/master/docs/logos/round-orange-196x196.png",
                "<b>Hello from .NET!</b><br/>The site is powered by .NET Core instead of PHP, compiled entirely to MSIL bytecode using <a href='https://www.peachpie.io/' target='_blank'>PeachPie</a>.");
        }

        static readonly string GeneratorHtml = "<meta name=\"generator\" content=\"WpDotNet (PeachPie) " + InformationalVersion + " \" />";

        public void Configure(IWpApp app)
        {
            //
            // Dashboard:
            //

            // add information into Right Now section
            app.DashboardRightNow(GetDashboardRightNow);

            // do not allow editing of .php files:
            app.AddFilter("editable_extensions", new Func<IList, IList>(editable_extensions =>
            {
                editable_extensions.Remove("php");
                return editable_extensions;
            }));

            // TODO: "install_plugins_upload" to customize upload plugin form
            // TODO: filter query_plugins

            //
            // Blogs:
            //

            // alter generator metadata
            app.AddFilter("get_the_generator_xhtml", new Func<string>(() => GeneratorHtml));
            // add analytics
        }
    }
}
