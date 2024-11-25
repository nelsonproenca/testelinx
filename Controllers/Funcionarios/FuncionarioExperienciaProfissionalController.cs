using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.ExperienciaProfissional.Commands;
using RVC.Intranet4.Funcionarios.Application.ExperienciaProfissional.Models;
using RVC.Intranet4.Funcionarios.Application.ExperienciaProfissional.Queries;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Models;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioExperienciaProfissionalController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioExperienciaProfissionalController : ControllerBase
    {
        private readonly IExperienciaProfissionalCommands experienciaProfissionalCommands;
        private readonly IExperienciaProfissionalQueries experienciaProfissionalQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioExperienciaProfissionalController"/> class.
        /// </summary>
        /// <param name="experienciaProfissionalCommands">Funcionario Commands interface.</param>
        /// <param name="experienciaProfissionalQueries">Funcionario interface.</param>
        public FuncionarioExperienciaProfissionalController(
            IExperienciaProfissionalCommands experienciaProfissionalCommands,
            IExperienciaProfissionalQueries experienciaProfissionalQueries)
        {
            this.experienciaProfissionalCommands = experienciaProfissionalCommands;
            this.experienciaProfissionalQueries = experienciaProfissionalQueries;
        }

        /// <summary>
        /// GetAsync método Buscar Por Funcionario.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/FuncionarioArquivos/2
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetPorFuncionarioAsync(int codigoFuncionario)
        {
            var entidades = await experienciaProfissionalQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(entidades);
        }

        /// <summary>
        /// GetAsync método Buscar Um código experiencia.
        /// </summary>
        /// <param name="codigo">´código experiência.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("{codigo}")]
        
        public async Task<IActionResult> GetAsync(int codigo)
        {
            var entidade = await experienciaProfissionalQueries.BuscarUmAsync(codigo);

            return Ok(entidade);
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
        
        public async Task<IActionResult> InsertAsync([FromBody] ExperienciaProfissionalModel value)
        {
            var entidade = await experienciaProfissionalCommands.InserirAsync(value);

            return Ok(entidade);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método altera um registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>
        [HttpPut("{codigo}")]
        
        public async Task<IActionResult> UpdateAsync([FromBody] ExperienciaProfissionalModel value, int codigo)
        {
            var entidade = await experienciaProfissionalCommands.AlterarAsync(value, codigo);

            return Ok(entidade);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<AvaliacaoController>/5
        [HttpDelete("{codigo}")]
        
        public async Task<IActionResult> DeleteExperienciaFuncionarioAsync(int codigo)
        {
            var entidade = await experienciaProfissionalCommands.ExcluirAsync(codigo);

            return Ok(entidade);
        }
    }
}
