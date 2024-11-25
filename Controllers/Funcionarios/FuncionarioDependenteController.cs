using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Dependente.Commands;
using RVC.Intranet4.Funcionarios.Application.Dependente.Models;
using RVC.Intranet4.Funcionarios.Application.Dependente.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioDependenteController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioDependenteController : ControllerBase
    {
        private readonly IDependenteCommands funcionarioDependenteCommands;
        private readonly IDependenteQueries funcionarioDependenteQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioDependenteController"/> class.
        /// </summary>
        /// <param name="funcionarioDependenteCommands">FuncionarioCommands interface.</param>
        /// <param name="funcionarioDependenteQueries">FuncionarioQueries interface.</param>
        public FuncionarioDependenteController(
            IDependenteCommands funcionarioDependenteCommands,
            IDependenteQueries funcionarioDependenteQueries)
        {
            this.funcionarioDependenteCommands = funcionarioDependenteCommands;
            this.funcionarioDependenteQueries = funcionarioDependenteQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioDependentesAsync.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioDependenteController>/funcionario/2
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioDependentesAsync(int codigoFuncionario)
        {
            var funcionarioDependentes = await funcionarioDependenteQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(funcionarioDependentes);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoDependente">código do dependente.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioDependenteController>/2
        [HttpGet("{codigoDependente}")]
        
        public async Task<IActionResult> GetFuncionarioDependenteAsync(int codigoDependente)
        {
            var funcionarioDependente = await funcionarioDependenteQueries.BuscarUmAsync(codigoDependente);

            if (funcionarioDependente == null)
            {
                return NotFound();
            }

            return Ok(funcionarioDependente);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioDependenteController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        
        public async Task<IActionResult> InsertDependenteFuncionarioAsync([FromBody] DependenteModel value)
        {
            var novoDependenteFuncionario = await funcionarioDependenteCommands.InserirAsync(value);

            if (novoDependenteFuncionario.Erros.Length > 0)
            {
                return BadRequest(novoDependenteFuncionario.Erros);
            }

            if (novoDependenteFuncionario.Mensagem?.Length > 0)
            {
                return BadRequest(novoDependenteFuncionario.Mensagem);
            }

            return Ok(novoDependenteFuncionario.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioDependenteController>
        [HttpPut]
        
        public async Task<IActionResult> UpdateDependenteFuncionarioAsync([FromBody] DependenteModel value)
        {
            var alterarDependenteFuncionario = await funcionarioDependenteCommands.AlterarAsync(value);

            if (alterarDependenteFuncionario == false)
            {
                return BadRequest("Erro ao alterar dependente do funcionário");
            }

            return Ok("Sucesso");
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<FuncionarioDependenteController>/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteDependenteFuncionarioAsync(int id)
        {
            var dependenteExcluido = await funcionarioDependenteCommands.ExcluirAsync(id);

            if (!dependenteExcluido)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
