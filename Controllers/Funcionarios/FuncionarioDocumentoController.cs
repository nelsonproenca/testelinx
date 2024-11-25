using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Commands;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Models;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// AvaliacaoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioDocumentoController : ControllerBase
    {
        private readonly IFuncionarioDocumentoCommands funcionarioDocumentoCommands;
        private readonly IFuncionarioDocumentoQueries funcionarioDocumentoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioDocumentoController"/> class.
        /// </summary>
        /// <param name="funcionarioDocumentoCommands">AvaliacaoCommands interface.</param>
        /// <param name="funcionarioDocumentoQueries">AvaliacaoQueries interface.</param>
        public FuncionarioDocumentoController(
            IFuncionarioDocumentoCommands funcionarioDocumentoCommands,
            IFuncionarioDocumentoQueries funcionarioDocumentoQueries)
        {
            this.funcionarioDocumentoCommands = funcionarioDocumentoCommands;
            this.funcionarioDocumentoQueries = funcionarioDocumentoQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioDocumentoAsync.
        /// </summary>
        /// <param name="codigoDocumento">código do documento.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("GetFuncionarioDocumentoAsync/{codigoDocumento}")]
        public async Task<IActionResult> GetFuncionarioDocumentoAsync(int codigoDocumento)
        {
            var funcionarioDocumento = await funcionarioDocumentoQueries.BuscarFuncionarioDocumentoAsync(codigoDocumento);

            if (funcionarioDocumento == null)
            {
                return NotFound();
            }

            return Ok(funcionarioDocumento);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> InsertDocumentoFuncionarioAsync([FromBody] DocumentoModel value)
        {
            var novoDocumentoFuncionario = await funcionarioDocumentoCommands.InserirAsync(value);

            if (novoDocumentoFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoDocumentoFuncionario.Erros);
            }

            if (novoDocumentoFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoDocumentoFuncionario.Mensagem);
            }

            return Ok(novoDocumentoFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut]
        public async Task<IActionResult> UpdateDocumentoFuncionarioAsync([FromBody] DocumentoModel value)
        {
            var alterarDocumentoFuncionario = await funcionarioDocumentoCommands.AlterarAsync(value);

            if (alterarDocumentoFuncionario == false)
            {
                return BadRequest("Erro ao alterar documento do funcionário");
            }

            return Ok("Sucesso");
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<AvaliacaoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentoFuncionarioAsync(int id)
        {
            var documentoExcluido = await funcionarioDocumentoCommands.ExcluirAsync(id);

            if (!documentoExcluido)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
