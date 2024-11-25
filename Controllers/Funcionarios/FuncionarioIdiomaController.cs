using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Idioma.Commands;
using RVC.Intranet4.Funcionarios.Application.Idioma.Models;
using RVC.Intranet4.Funcionarios.Application.Idioma.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioIdiomaController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioIdiomaController : ControllerBase
    {
        private readonly IIdiomaCommands funcionarioIdiomaCommands;
        private readonly IIdiomaQueries funcionarioIdiomaQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioIdiomaController"/> class.
        /// </summary>
        /// <param name="funcionarioIdiomaCommands">AvaliacaoCommands interface.</param>
        /// <param name="funcionarioIdiomaQueries">AvaliacaoQueries interface.</param>
        public FuncionarioIdiomaController(
            IIdiomaCommands funcionarioIdiomaCommands,
            IIdiomaQueries funcionarioIdiomaQueries)
        {
            this.funcionarioIdiomaCommands = funcionarioIdiomaCommands;
            this.funcionarioIdiomaQueries = funcionarioIdiomaQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioIdiomasAsync(int codigoFuncionario)
        {
            var funcionarioIdiomas = await funcionarioIdiomaQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(funcionarioIdiomas);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioIdiomaAsync.
        /// </summary>
        /// <param name="codigoIdioma">numero do idioma.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/FuncionarioArquivos/2
        [HttpGet("{codigoIdioma}")]
        
        public async Task<IActionResult> GetFuncionarioIdiomaAsync(int codigoIdioma)
        {
            var funcionarioIdioma = await funcionarioIdiomaQueries.BuscarUmAsync(codigoIdioma);

            if (funcionarioIdioma == null)
            {
                return NotFound();
            }

            return Ok(funcionarioIdioma);
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
        
        public async Task<IActionResult> InsertIdiomaFuncionarioAsync([FromBody] IdiomaModel value)
        {
            var novoIdiomaFuncionario = await funcionarioIdiomaCommands.InserirAsync(value);

            if (novoIdiomaFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoIdiomaFuncionario.Erros);
            }

            if (novoIdiomaFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoIdiomaFuncionario.Mensagem);
            }

            return Ok(novoIdiomaFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut]
        
        public async Task<IActionResult> UpdateExperienciaFuncionarioAsync([FromBody] IdiomaModel value)
        {
            var alterarIdiomaFuncionario = await funcionarioIdiomaCommands.AlterarAsync(value);

            if (alterarIdiomaFuncionario == false)
            {
                return BadRequest("Erro ao alterar idioma do funcionário");
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
        
        public async Task<IActionResult> DeleteIdiomaFuncionarioAsync(int id)
        {
            var idiomaExcluida = await funcionarioIdiomaCommands.ExcluirAsync(id);

            if (!idiomaExcluida)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
