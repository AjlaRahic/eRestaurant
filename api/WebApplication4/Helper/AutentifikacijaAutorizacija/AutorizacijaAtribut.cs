using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Helper.AutentifikacijaAutorizacija
{
    public class AutorizacijaAtribut : TypeFilterAttribute
    {
        public AutorizacijaAtribut(bool admin, bool korisnik, bool uposlenik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _admin;
        private readonly bool _uposlenik;
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.GetLoginInfo().isLogiran)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }
            KretanjePoSistemu.Save(filterContext.HttpContext);
            if (filterContext.HttpContext.GetLoginInfo().isPermsijaKorisnik)
            {
                return;
            }
            if (filterContext.HttpContext.GetLoginInfo().isPermsijaAdministrator && _admin)
            {
                return;
            }
            if (filterContext.HttpContext.GetLoginInfo().isPermisijaUposlenik && _uposlenik)
            {
                return;
            }

            filterContext.Result = new UnauthorizedResult();
        }
    }
}
