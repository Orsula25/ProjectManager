using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManager.ASPMVC.Handlers;

namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class ManagerOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            UserSessionManager session = context.HttpContext.RequestServices.GetService(typeof(UserSessionManager)) as UserSessionManager;

            if (session?.EmployeeId is null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (!session.IsManager)
            {
                context.Result = new RedirectToActionResult("Index", "Project", null);
            }
        }
    }
}
