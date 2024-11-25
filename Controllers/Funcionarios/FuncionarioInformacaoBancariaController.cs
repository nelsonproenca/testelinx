using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.InformacaoBancaria.Commands;
using RVC.Intranet4.Funcionarios.Application.InformacaoBancaria.Models;
using RVC.Intranet4.Funcionarios.Application.InformacaoBancaria.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// AvaliacaoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioInformacaoBancariaController : ControllerBase
    {
        private readonly IInformacaoBancariaCommands informacaoBancariaCommands;
        private readonly IInformacaoBancariaQueries informacaoBancariaQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioInformacaoBancariaController"/> class.
        /// </summary>
        /// <param name="informacaoBancariaCommands">FuncionarioCommands interface.</param>
        /// <param name="informacaoBancariaQueries">FuncionarioQueries interface.</param>
        public FuncionarioInformacaoBancariaController(
            IInformacaoBancariaCommands informacaoBancariaCommands,
            IInformacaoBancariaQueries informacaoBancariaQueries)
        {
            this.informacaoBancariaCommands = informacaoBancariaCommands;
            this.informacaoBancariaQueries = informacaoBancariaQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionario.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("porFuncionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioInformacaoBancariaAsync(int codigoFuncionario)
        {
            var informacaoBancaria = await informacaoBancariaQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(informacaoBancaria);
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
        
        public async Task<IActionResult> InsertInformacaoBancariaFuncionarioAsync([FromBody] InformacaoBancariaModel value)
        {
            var novoInformacaoBancaria = await informacaoBancariaCommands.InserirAsync(value);

            return Ok(novoInformacaoBancaria.Item);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateInformacaoBancariaFuncionarioAsync([FromBody] InformacaoBancariaModel value)
        {
            var alterarEntidade = await informacaoBancariaCommands.AlterarAsync(value);

            return Ok(alterarEntidade);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<AvaliacaoController>/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteInformacaoBancariaFuncionarioAsync(int id)
        {
            var entidadeExcluido = await informacaoBancariaCommands.ExcluirAsync(id);

            return Ok(entidadeExcluido);
        }
    }
}
