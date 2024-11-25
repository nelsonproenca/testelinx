using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Endereco.Commands;
using RVC.Intranet4.Funcionarios.Application.Endereco.Models;
using RVC.Intranet4.Funcionarios.Application.Endereco.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioEnderecoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioEnderecoController : ControllerBase
    {
        private readonly IEnderecoCommands funcionarioEnderecoCommands;
        private readonly IEnderecoQueries funcionarioEnderecoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioEnderecoController"/> class.
        /// </summary>
        /// <param name="funcionarioEnderecoCommands">FuncionarioCommands interface.</param>
        /// <param name="funcionarioEnderecoQueries">FuncionarioQueries interface.</param>
        public FuncionarioEnderecoController(
            IEnderecoCommands funcionarioEnderecoCommands,
            IEnderecoQueries funcionarioEnderecoQueries)
        {
            this.funcionarioEnderecoCommands = funcionarioEnderecoCommands;
            this.funcionarioEnderecoQueries = funcionarioEnderecoQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioEnderecosAsync.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioEnderecoController>/funcionario/2
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioEnderecosAsync(int codigoFuncionario)
        {
            var funcionarioEnderecos = await funcionarioEnderecoQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(funcionarioEnderecos);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioEnderecoAsync.
        /// </summary>
        /// <param name="codigoEndereco">código do endereço.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioEnderecoController>/2
        [HttpGet("{codigoEndereco}")]
        
        public async Task<IActionResult> GetFuncionarioEnderecoAsync(int codigoEndereco)
        {
            var funcionarioEndereco = await funcionarioEnderecoQueries.BuscarUmAsync(codigoEndereco);

            if (funcionarioEndereco == null)
            {
                return NotFound();
            }

            return Ok(funcionarioEndereco);
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
        
        public async Task<IActionResult> InsertEnderecoFuncionarioAsync([FromBody] EnderecoModel value)
        {
            var novoEnderecoFuncionario = await funcionarioEnderecoCommands.InserirAsync(value);

            if (novoEnderecoFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoEnderecoFuncionario.Erros);
            }

            if (novoEnderecoFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoEnderecoFuncionario.Mensagem);
            }

            return Ok(novoEnderecoFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<AvaliacaoController>
        [HttpPut]
        
        public async Task<IActionResult> UpdateEnderecoFuncionarioAsync([FromBody] EnderecoModel value)
        {
            var alterarEnderecoFuncionario = await funcionarioEnderecoCommands.AlterarAsync(value);

            return Ok(alterarEnderecoFuncionario);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<AvaliacaoController>/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteEnderecoFuncionarioAsync(int id)
        {
            var enderecoExcluido = await funcionarioEnderecoCommands.ExcluirAsync(id);

            if (!enderecoExcluido)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
