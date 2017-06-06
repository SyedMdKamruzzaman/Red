using System.Web.Mvc;
using BlackSys.Filter;

namespace BlackSys
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
            //filters.Add(new AccessValidationAttribute());

        }
    }
}