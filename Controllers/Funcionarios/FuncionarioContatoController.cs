using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Contato.Commands;
using RVC.Intranet4.Funcionarios.Application.Contato.Models;
using RVC.Intranet4.Funcionarios.Application.Contato.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// AvaliacaoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioContatoController : ControllerBase
    {
        private readonly IContatoCommands funcionarioContatoCommands;
        private readonly IContatoQueries funcionarioContatoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioContatoController"/> class.
        /// </summary>
        /// <param name="funcionarioContatoCommands">FuncionarioCommands interface.</param>
        /// <param name="funcionarioContatoQueries">FuncionarioQueries interface.</param>
        public FuncionarioContatoController(
            IContatoCommands funcionarioContatoCommands,
            IContatoQueries funcionarioContatoQueries)
        {
            this.funcionarioContatoCommands = funcionarioContatoCommands;
            this.funcionarioContatoQueries = funcionarioContatoQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioContatos.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioContatoController>/funcionario/2
        [HttpGet("contatos/{codigoFuncionario}")]
        public async Task<IActionResult> GetFuncionarioContatosAsync(int codigoFuncionario)
        {
            var funcionarioContatos = await funcionarioContatoQueries.BuscarContatosAsync(codigoFuncionario);

            if (funcionarioContatos == null)
            {
                return NotFound();
            }

            return Ok(funcionarioContatos);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioContato.
        /// </summary>
        /// <param name="codigoContato">código do contato.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("contato/{codigoContato}")]
        public async Task<IActionResult> GetFuncionarioContatoAsync(int codigoContato)
        {
            var funcionarioContato = await funcionarioContatoQueries.BuscarContatoAsync(codigoContato);

            if (funcionarioContato == null)
            {
                return NotFound();
            }

            return Ok(funcionarioContato);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> InsertContatoFuncionarioAsync([FromBody] ContatoModel value)
        {
            var novoContatoFuncionario = await funcionarioContatoCommands.InserirAsync(value);

            if (novoContatoFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoContatoFuncionario.Erros);
            }

            if (novoContatoFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoContatoFuncionario.Mensagem);
            }

            return Ok(novoContatoFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut]
        public async Task<IActionResult> UpdateContatoFuncionarioAsync([FromBody] ContatoModel value)
        {
            var alterarContatoFuncionario = await funcionarioContatoCommands.AlterarAsync(value);

            if (alterarContatoFuncionario == false)
            {
                return BadRequest("Erro ao alterar contato do funcionário");
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
        public async Task<IActionResult> DeleteContatoFuncionarioAsync(int id)
        {
            var contatoExcluido = await funcionarioContatoCommands.ExcluirAsync(id);

            if (!contatoExcluido)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}