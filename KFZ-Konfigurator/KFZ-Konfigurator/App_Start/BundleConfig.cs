﻿using System.Web;
using System.Web.Optimization;

namespace KFZ_Konfigurator
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapVue").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/vue.js",
                      "~/Scripts/underscore-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/out/site.css"));

            //Views
            bundles.Add(new ScriptBundle("~/bundles/model").Include(
                      "~/Views/Model/Index.bundle.js"));
            bundles.Add(new ScriptBundle("~/bundles/engineSettings").Include(
                      "~/Views/EngineSettings/Index.bundle.js"));
            bundles.Add(new ScriptBundle("~/bundles/accessories").Include(
                      "~/Views/Accessories/Index.bundle.js"));
        }
    }
}
