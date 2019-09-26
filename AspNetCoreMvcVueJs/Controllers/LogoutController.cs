using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreMvcAngular.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    public class LogoutController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpPost]
        [Route("")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("OpenIdConnect");

            return Ok();
        }
    }
}
