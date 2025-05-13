using Microsoft.AspNetCore.Mvc;

namespace ApiSabWap.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public bool VerificaToken(string security)
        {
            if (!HttpContext.Request.Headers.Any(a => a.Key == "Security") && string.IsNullOrEmpty(security))
            {
                return false;
            }

            var token = HttpContext.Request.Headers.FirstOrDefault(a => a.Key == "Security").Value;

            if (string.IsNullOrEmpty(token))
            {
                var key = System.Web.HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value).Get("Security");

                token = key;
            }

            var segredo = "D820BFEB940EA420FFE7938AE1DB";

            string pass;
            if (security == null)
                pass = "";
            else
                pass = security;

            if (token != segredo && segredo != pass)
            {
                return false;
            }

            return true;

        }
    }
}
