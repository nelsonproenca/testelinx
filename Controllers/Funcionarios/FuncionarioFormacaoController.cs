using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Formacao.Commands;
using RVC.Intranet4.Funcionarios.Application.Formacao.Models;
using RVC.Intranet4.Funcionarios.Application.Formacao.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioFormacaoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioFormacaoController : ControllerBase
    {
        private readonly IFormacaoCommands formacaoCommands;
        private readonly IFormacaoQueries formacaoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioFormacaoController"/> class.
        /// </summary>
        /// <param name="funcionarioFormacaoCommands">AvaliacaoCommands interface.</param>
        /// <param name="funcionarioFormacaoQueries">AvaliacaoQueries interface.</param>
        public FuncionarioFormacaoController(
            IFormacaoCommands funcionarioFormacaoCommands,
            IFormacaoQueries funcionarioFormacaoQueries)
        {
            this.formacaoCommands = funcionarioFormacaoCommands;
            this.formacaoQueries = funcionarioFormacaoQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioFormacoesAsync(int codigoFuncionario)
        {
            var funcionarioFormacoes = await formacaoQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(funcionarioFormacoes);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoFormacao">código da formação.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("{codigoFormacao}")]
        
        public async Task<IActionResult> GetFormacaoAsync(int codigoFormacao)
        {
            var funcionarioFormacao = await formacaoQueries.BuscarUmAsync(codigoFormacao);

            return Ok(funcionarioFormacao);
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
        
        public async Task<IActionResult> InsertFormacaoFuncionarioAsync([FromBody] FormacaoModel value)
        {
            var novaFormacaoFuncionario = await formacaoCommands.InserirAsync(value);

            return Ok(novaFormacaoFuncionario);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut("{codigo}")]
        
        public async Task<IActionResult> UpdateAsync([FromBody] FormacaoModel value, int codigo)
        {
            var alterarFormacaoFuncionario = await formacaoCommands.AlterarAsync(value, codigo);

            return Ok(alterarFormacaoFuncionario);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<AvaliacaoController>/5
        [HttpDelete("{codigo}")]
        
        public async Task<IActionResult> DeleteFormacaoFuncionarioAsync(int codigo)
        {
            var formacaoExcluida = await formacaoCommands.ExcluirAsync(codigo);

            return Ok(formacaoExcluida);
        }
    }
}
