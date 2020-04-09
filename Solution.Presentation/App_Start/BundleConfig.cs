using System.Web;
using System.Web.Optimization;

namespace Solution.Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/template/js").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-ui.min.js",
                "~/Scripts/jquery.countdown.min.js",
                "~/Scripts/jquery.nice-select.min.js",
                "~/Scripts/jquery.zoom.min.js",
                "~/Scripts/jquery.dd.min.js",
                "~/Scripts/jquery.slicknav.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/main.js"
                ));

            bundles.Add(new StyleBundle("~/template/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/themify-icons.css",
                      "~/Content/css/elegant-icons.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/nice-select.css",
                      "~/Content/css/jquery-ui.min.css",
                      "~/Content/css/slicknav.min.css",
                      "~/Content/css/style.css"));
        }
    }
}
