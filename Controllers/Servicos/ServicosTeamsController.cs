using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.Teams.Interfaces;
using RVC.Intranet4.Core.Infrastructure.Teams.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosTeamsController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosTeamsController : ControllerBase
    {
        private readonly ITeamsService teamsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosTeamsController"/> class.
        /// </summary>
        /// <param name="teamsService">TeamsService.</param>
        public ServicosTeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        /// <summary>
        /// Enviar Teams (Async).
        /// </summary>
        /// <param name="value">entidade.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<TimelineController>/5
        [HttpPost]
        
        public async Task<IActionResult> PostAsync([FromBody] TeamsModel value)
        {
            var enviado = await teamsService.EnviarMensagemAsync(value);

            return Ok(enviado);
        }
    }
}
