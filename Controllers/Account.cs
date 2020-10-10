using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "/") => Ok(/*new LoginModel { returnUrl = returnUrl }*/);
    }
}
