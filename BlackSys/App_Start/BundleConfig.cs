using System.Web;
using System.Web.Optimization;

namespace BlackSys
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*STYLES*/
            //bundles.Add(new StyleBundle("~/Content/font-awensome").Include("~/Content/plugins/font-awesome/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/simple-icons").Include("~/Content/plugins/simple-line-icons/simple-line-icons.min.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                  "~/Content/plugins/font-awesome/css/font-awesome.min.css",
                "~/Content/plugins/bootstrap/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-switch").Include(
                "~/Content/plugins/bootstrap-switch/css/bootstrap-switch.min.css"));

            bundles.Add(new StyleBundle("~/Content/uniform").Include("~/Content/plugins/uniform/css/uniform.default.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/plugins/jquery-{version}.js",
                 "~/Scripts/jquery.validate*",
                 "~/Scripts/jquery.unobtrusive-ajax.js",
                 "~/Scripts/respond.js",
                 "~/Scripts/jquery.maskedinput.min.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Content/plugins/jquery-validation/js/jquery.validate.js",
                  "~/Scripts/agiletech-common.js"
                ));            
            bundles.Add(new ScriptBundle("~/bundles/jquery-migrate").Include("~/Content/plugins/jquery-migrate.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include("~/Content/plugins/jquery-ui/jquery-ui.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Content/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Content/plugins/bootstrap-daterangepicker/moment.min.js",
                "~/Content/plugins/bootstrap-daterangepicker/daterangepicker.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jquery-plugins").Include(
            "~/Content/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
            "~/Content/plugins/jquery.blockui.min.js",
            "~/Content/plugins/jquery.cokie.min.js",
            "~/Content/plugins/uniform/jquery.uniform.min.js",
            "~/Content/plugins/jquery.pulsate.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                   "~/Scripts/DataTables/jquery.dataTables.js"));

            bundles.Add(new StyleBundle("~/Content/datatables").Include(
                      "~/Content/DataTables/css/jquery.dataTables.css"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Content/template/scripts/app.js",
                "~/Content/template/layout/scripts/layout.js",
                "~/Content/template/layout/scripts/demo.js"
              
                ));
            bundles.Add(new ScriptBundle("~/bundles/tabs").Include(
               "~/Content/tabs/js/easyResponsiveTabs.js",
               "~/Content/tabs/js/jquery-1.9.1.min.js"
               ));

            bundles.Add(new StyleBundle("~/Content/tabs").Include(
                "~/Content/tabs/css/easy-responsive-tabs.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquerytoken").Include(
                   "~/Scripts/JQueryToken/jquery.tokeninput.js"));

            bundles.Add(new StyleBundle("~/Content/jquerytoken").Include(
               "~/Content/JQueryToken/token-input.css",
                  "~/Content/JQueryToken/token-input-facebook.css"
                  ));
            bundles.Add(new ScriptBundle("~/bundles/angularJS").Include(
               "~/Scripts/angular/angular.js",
               "~/Scripts/angular/angularApp.js"
               ));
            BundleTable.EnableOptimizations = false;
        }
    }
}