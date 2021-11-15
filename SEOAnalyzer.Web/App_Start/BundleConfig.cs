using System.Web;
using System.Web.Optimization;

namespace SEOAnalyzer.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-3.6.0.min.js",
                        "~/Scripts/jquery.dataTables-1.11.3.min.js",
                        "~/Scripts/loadingoverlay-2.1.7.min.js",
                        "~/Scripts/site.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap-5.0.2.min.css",
                      "~/Content/css/dataTables.bootstrap5.min.css",
                      "~/Content/css/jquery.dataTables-1.11.3.min.css",
                      "~/Content/css/site.css"));
        }
    }
}
