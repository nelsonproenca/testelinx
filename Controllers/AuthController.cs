using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Web.Services;

namespace RVC.Intranet4.Web.Controllers
{
    /// <summary>
    /// AuthController.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Authenticate.
        /// </summary>
        /// <returns>token.</returns>
        [HttpGet("token")]
        public ActionResult Authenticate()
        {
            var token = TokenService.GenerateToken();

            // Retorna os dados
            return Ok(token);
        }
    }
}
