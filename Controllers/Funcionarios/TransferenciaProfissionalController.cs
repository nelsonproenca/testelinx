using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.TransferenciaProfissional.Commands;
using RVC.Intranet4.Funcionarios.Application.TransferenciaProfissional.Models;
using RVC.Intranet4.Funcionarios.Application.TransferenciaProfissional.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// TransferenciaProfissional Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class TransferenciaProfissionalController : ControllerBase
    {
        private readonly ITransferenciaProfissionalCommands transferenciaProfissionalCommands;
        private readonly ITransferenciaProfissionalQueries transferenciaProfissionalQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransferenciaProfissionalController"/> class.
        /// </summary>
        /// <param name="transferenciaProfissionalCommands">transferenciaProfissionalCommands.</param>
        /// <param name="transferenciaProfissionalQueries">transferenciaProfissionalQueries.</param>
        public TransferenciaProfissionalController(
            ITransferenciaProfissionalCommands transferenciaProfissionalCommands, 
            ITransferenciaProfissionalQueries transferenciaProfissionalQueries)
        {
            this.transferenciaProfissionalCommands = transferenciaProfissionalCommands;
            this.transferenciaProfissionalQueries = transferenciaProfissionalQueries;
        }

        /// <summary>
        /// GetAsync método BuscarTodos com paginação.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <param name="emailProfissional">emailProfissional.</param>
        /// <param name="emailCoach">emailCoach.</param>
        /// <param name="emailLider">emailLider.</param>
        /// <param name="dataSolicitacao">dataSolicitacao.</param>
        /// <param name="statusAndamento">statusAndamento.</param>
        /// <param name="flagRH">flagRH.</param>
        /// <param name="emailLiderDestino">emailLiderDestino.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<TransferenciaProfissionalController>
        [HttpGet("filtros")]
        public async Task<IActionResult> GetAsync(int codigo = 0, string emailProfissional = "", string emailCoach = "", string emailLider = "", string dataSolicitacao = "", string statusAndamento = "", bool? flagRH = null, string emailLiderDestino = "")
        {
            var transferenciaProfissionais = await transferenciaProfissionalQueries.BuscarPorFiltroAsync(codigo, emailProfissional, emailCoach, emailLider, dataSolicitacao, statusAndamento, flagRH, emailLiderDestino);

            return Ok(transferenciaProfissionais);
        }

        /// <summary>
        /// GetAsync método BuscarUm por codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<TransferenciaProfissionalController>/5
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetAsync(int codigo)
        {
            var transferenciaProfissional = await transferenciaProfissionalQueries.BuscarUmAsync(codigo);

            return Ok(transferenciaProfissional);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<TransferenciaProfissionalController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TransferenciaProfissionalModel value)
        {
            var novaTransferenciaProfissional = await transferenciaProfissionalCommands.InserirAsync(value);
           
            return Ok(novaTransferenciaProfissional);
        }

        /// <summary>
        /// PutAsync método Alterar um registro.
        /// </summary>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<TransferenciaProfissionalController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TransferenciaProfissionalModel value)
        {
            var transferenciaProfissionalAlterada = await transferenciaProfissionalCommands.AlterarAsync(value);

            return Ok(transferenciaProfissionalAlterada);
        }

        /// <summary>
        /// PATCH método Alterar partes de um registro.
        /// </summary>
        /// <param name="value">entidade para alterar.</param>
        /// <param name="codigoTransferencia">codigoTransferencia.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PATCH api/<TransferenciaProfissionalController>/5
        [HttpPatch("{codigoTransferencia}")]
        public async Task<IActionResult> PatchAsync([FromBody] TransferenciaProfissionalModel value, int codigoTransferencia)
        {
            var transferenciaProfissionalAlterada = await transferenciaProfissionalCommands.AlterarParcialAsync(value, codigoTransferencia);

            return Ok(transferenciaProfissionalAlterada);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<TransferenciaProfissionalController>/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteAsync(int codigo)
        {
            var transferenciaProfissionalExcluida = await transferenciaProfissionalCommands.ExcluirAsync(codigo);

            return Ok(transferenciaProfissionalExcluida);
        }
    }
}
