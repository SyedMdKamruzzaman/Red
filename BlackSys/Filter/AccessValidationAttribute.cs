using Microsoft.AspNet.Identity.EntityFramework;
using BlackSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Blacksys.Controllers;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace BlackSys.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                 || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization == false && filterContext.IsChildAction == false)
            {
                string controllerName = filterContext.RouteData.Values["controller"].ToString();                
                string actionName = filterContext.RouteData.Values["action"].ToString();

                bool isAjaxChildActionOnlyAttribute = filterContext.ActionDescriptor.IsDefined(typeof(AjaxChildActionOnlyAttribute), inherit: true)
                                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AjaxChildActionOnlyAttribute), inherit: true);


                string[] AllowedAction = new[] { "HOME", "LOGOFF", "CHANGEPASSWORD" };
                if (!AllowedAction.Contains(actionName.ToUpper()))
                {
                    //if (GetExpectedReturnType(filterContext).Name != "JsonResult")
                    //{
                    if (isAjaxChildActionOnlyAttribute==false)
                    {
                        bool varified = CheckAuthorization(controllerName, actionName);
                        if (varified == false)
                        {
                            //AuthenticationManager.SignOut();
                            filterContext.Result = new RedirectToRouteResult(
                                new System.Web.Routing.RouteValueDictionary(
                                new { action = "Home", controller = "Account", area = "Security" }));
                        }
                }
                    //}
                }
            }
            base.OnActionExecuting(filterContext);
        }
        private Type GetExpectedReturnType(ActionExecutingContext filterContext)
        {
            // Find out what type is expected to be returned
            string actionName = filterContext.ActionDescriptor.ActionName;
            Type controllerType = filterContext.Controller.GetType();
            MethodInfo actionMethodInfo = default(MethodInfo);
            try
            {
                actionMethodInfo = controllerType.GetMethod(actionName);
            }
            catch (AmbiguousMatchException ex)
            {
                // Try to find a match using the parameters passed through
                var actionParams = filterContext.ActionParameters;
                List<Type> paramTypes = new List<Type>();
                foreach (var p in actionParams)
                {
                    paramTypes.Add(p.Value.GetType());
                }

                actionMethodInfo = controllerType.GetMethod(actionName, paramTypes.ToArray());
            }

            return actionMethodInfo.ReturnType;
        }

        private Boolean CheckAuthorization1(string controllerName, string actionName)
        {         
                //ApplicationDbContext db = new ApplicationDbContext();
                //ApplicationUser appUser = db.Users.Where(d => d.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
                //ICollection<IdentityUserRole> usersInRoles = appUser.Roles;
                //List<string> RoleIdList = usersInRoles.Select(d => d.RoleId).ToList();

                //List<Menu> parentMenu = (from sm in db.Men
                //                             join rm in db.Roles
                //                             on sm.MenuId equals rm.MenuId
                //                             where RoleIdList.Contains(rm.RoleId) && sm.Controller == controllerName && sm.Action == actionName
                //                             orderby sm.SLNo
                //                             select sm).ToList();
            BlackSysEntities db = new BlackSysEntities();
            
            IEnumerable<Permission> menus = db.Database.SqlQuery<Permission>("SP_GetMenu @UserId ='" + HttpContext.Current.User.Identity.GetUserId() + "', @RoleId = NULL");
            //ViewBag.Menus = from a in db.Menu
            //                where a.MenuID == 0
            //                select a;
            //var Permission = db.Permission.Include(r => r.Menu).Include(s => s.AspNetRoles);
            if (menus.ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }            
        }

        private Boolean CheckAuthorization(string controllerName, string actionName)
        {
            BlackSysEntities db = new BlackSysEntities();
            AspNetUsers appUser = db.AspNetUsers.Where(d => d.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ICollection<AspNetRoles> usersInRoles = appUser.AspNetRoles;
            List<string> RoleIdList = usersInRoles.Select(d => d.Id).ToList();

            List<Menu> parentMenu = (from sm in db.Menu
                                         join rm in db.Permission
                                         on sm.MenuID equals rm.MenuID
                                         where RoleIdList.Contains(rm.RoleID) && sm.MenuURL.Split('/')[0] == controllerName && sm.MenuURL.Split('/')[1] == actionName
                                         select sm).ToList();
            if (parentMenu.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}