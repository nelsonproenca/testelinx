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
    public class FuncionarioDetalheController : ControllerBase
    {
        private readonly IFuncionarioDetalheCommands funcionarioDetalheCommands;
        private readonly IFuncionarioDetalheQueries funcionarioDetalheQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioDetalheController"/> class.
        /// </summary>
        /// <param name="funcionarioDetalheCommands">AvaliacaoCommands interface.</param>
        /// <param name="funcionarioDetalheQueries">AvaliacaoQueries interface.</param>
        public FuncionarioDetalheController(
            IFuncionarioDetalheCommands funcionarioDetalheCommands,
            IFuncionarioDetalheQueries funcionarioDetalheQueries)
        {
            this.funcionarioDetalheCommands = funcionarioDetalheCommands;
            this.funcionarioDetalheQueries = funcionarioDetalheQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioDetalheAsync.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/FuncionarioArquivos/2
        [HttpGet("GetFuncionarioDetalheAsync/{codigoFuncionario}")]
        public async Task<IActionResult> GetFuncionarioDetalheAsync(int codigoFuncionario)
        {
            var funcionarioDetalhe = await funcionarioDetalheQueries.BuscarFuncionarioDetalheAsync(codigoFuncionario);

            if (funcionarioDetalhe == null)
            {
                return NotFound();
            }

            return Ok(funcionarioDetalhe);
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
        public async Task<IActionResult> InsertDetalheFuncionarioAsync([FromBody] DetalheModel value)
        {
            var novoDetalheFuncionario = await funcionarioDetalheCommands.InserirAsync(value);

            if (novoDetalheFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoDetalheFuncionario.Erros);
            }

            if (novoDetalheFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoDetalheFuncionario.Mensagem);
            }

            return Ok(novoDetalheFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut]
        public async Task<IActionResult> UpdateDetalheFuncionarioAsync([FromBody] DetalheModel value)
        {
            var alterarDetalheFuncionario = await funcionarioDetalheCommands.AlterarAsync(value);

            if (alterarDetalheFuncionario == false)
            {
                return BadRequest("Erro ao alterar detalhe do funcionário");
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
        public async Task<IActionResult> DeleteDetalheFuncionarioAsync(int id)
        {
            var detalheFuncionarioExcluido = await funcionarioDetalheCommands.ExcluirAsync(id);

            if (!detalheFuncionarioExcluido)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}