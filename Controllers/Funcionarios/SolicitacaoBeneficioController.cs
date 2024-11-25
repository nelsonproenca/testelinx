using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.SolicitacaoBeneficio.Commands;
using RVC.Intranet4.Funcionarios.Application.SolicitacaoBeneficio.Models;
using RVC.Intranet4.Funcionarios.Application.SolicitacaoBeneficio.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// SolicitacaoBeneficio Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class SolicitacaoBeneficioController : ControllerBase
    {
        private readonly ISolicitacaoBeneficioCommands solicitacaoBeneficioCommands;
        private readonly ISolicitacaoBeneficioQueries solicitacaoBeneficioQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolicitacaoBeneficioController"/> class.
        /// </summary>
        /// <param name="solicitacaoBeneficioCommands">SolicitacaoBeneficioCommands.</param>
        /// <param name="solicitacaoBeneficioQueries">SolicitacaoBeneficioQueries.</param>
        public SolicitacaoBeneficioController(
            ISolicitacaoBeneficioCommands solicitacaoBeneficioCommands, 
            ISolicitacaoBeneficioQueries solicitacaoBeneficioQueries)
        {
            this.solicitacaoBeneficioCommands = solicitacaoBeneficioCommands;
            this.solicitacaoBeneficioQueries = solicitacaoBeneficioQueries;
        }

        /// <summary>
        /// GetAsync método BuscarTodos com paginação.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <param name="codigoProfissional">codigoProfissional.</param>        
        /// <param name="flagRH">flagRH.</param>
        /// <param name="tipoBeneficio">tipoBeneficio.</param>
        /// <param name="statusBeneficio">statusBeneficio.</param>
        /// <param name="codigoLider">codigoLider.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<SolicitacaoBeneficioController>
        [HttpGet("filtros")]
        public async Task<IActionResult> GetAsync(int codigo = 0, int codigoProfissional = 0, bool? flagRH = null, string tipoBeneficio = "", string statusBeneficio = "", int codigoLider = 0)
        {
            var transferenciaProfissionais = await solicitacaoBeneficioQueries.BuscarPorFiltroAsync(codigo, codigoProfissional, flagRH, tipoBeneficio, statusBeneficio, codigoLider);

            return Ok(transferenciaProfissionais);
        }

        /// <summary>
        /// GetAsync método BuscarUm por codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<SolicitacaoBeneficioController>/5
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetAsync(int codigo)
        {
            var solicitacaoBeneficio = await solicitacaoBeneficioQueries.BuscarUmAsync(codigo);

            return Ok(solicitacaoBeneficio);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<SolicitacaoBeneficioController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SolicitacaoBeneficioModel value)
        {
            var novaSolicitacaoBeneficio = await solicitacaoBeneficioCommands.InserirAsync(value);
         
            return Ok(novaSolicitacaoBeneficio);
        }

        /// <summary>
        /// PutAsync método Alterar um registro.
        /// </summary>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<SolicitacaoBeneficioController>/5
        [HttpPut]
        
        public async Task<IActionResult> PutAsync([FromBody] SolicitacaoBeneficioModel value)
        {
            var solicitacaoBeneficioAlterada = await solicitacaoBeneficioCommands.AlterarAsync(value);

            return Ok(solicitacaoBeneficioAlterada);
        }

        /// <summary>
        /// PATCH método Alterar partes de um registro.
        /// </summary>
        /// <param name="value">entidade para alterar.</param>
        /// <param name="codigo">codigo solicitacao.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PATCH api/<SolicitacaoBeneficioController>/5
        [HttpPatch("{codigo}")]
        public async Task<IActionResult> PatchAsync([FromBody] SolicitacaoBeneficioModel value, int codigo)
        {
            var solicitacaoBeneficioAlterada = await solicitacaoBeneficioCommands.AlterarParcialAsync(value, codigo);

            return Ok(solicitacaoBeneficioAlterada);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<SolicitacaoBeneficioController>/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteAsync(int codigo)
        {
            var solicitacaoBeneficioExcluida = await solicitacaoBeneficioCommands.ExcluirAsync(codigo);

            return Ok(solicitacaoBeneficioExcluida);
        }
    }
}
