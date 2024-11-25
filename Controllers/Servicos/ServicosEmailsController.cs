using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.Emails.Interfaces;
using RVC.Intranet4.Core.Infrastructure.Emails.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosEmailsController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosEmailsController : ControllerBase
    {
        private readonly IEmailsService emailsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosEmailsController"/> class.
        /// </summary>
        /// <param name="emailsService">emailsService.</param>
        public ServicosEmailsController(IEmailsService emailsService)
        {
            this.emailsService = emailsService;
        }

        /// <summary>
        /// Enviar Emails (Async).
        /// </summary>
        /// <param name="value">entidade.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<TimelineController>/5
        [HttpPost]
        
        public async Task<IActionResult> PostAsync([FromBody] EmailModel value)
        {
            var enviado = await emailsService.SendEmailAsync(value);

            return Ok(enviado);
        }
    }
}
