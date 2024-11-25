using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.ReceitaFederal.Interfaces;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ReceitaController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosReceitaController : ControllerBase
    {
        private readonly IReceitaService receitaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosReceitaController"/> class.
        /// </summary>
        /// <param name="receitaService">receitaService.</param>
        public ServicosReceitaController(IReceitaService receitaService)
        {
            this.receitaService = receitaService;
        }

        /// <summary>
        /// Buscar Cnpj do Cliente na Receita.
        /// </summary>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<TimelineController>/5
        [HttpGet("{value}")]
        
        public async Task<IActionResult> GetAsync(string value)
        {
            var enviado = await receitaService.BuscarCnpjAsync(value);

            return Ok(enviado);
        }
    }
}
