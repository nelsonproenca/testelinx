using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Enums;
using RVC.Intranet4.Core.Infrastructure.FileShare.Models;
using RVC.Intranet4.Funcionarios.Application.Arquivo.Commands;
using RVC.Intranet4.Funcionarios.Application.Arquivo.Models;
using RVC.Intranet4.Funcionarios.Application.Arquivo.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioArquivoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioArquivoController : ControllerBase
    {
        private readonly IArquivoCommands arquivoCommands;
        private readonly IArquivoQueries arquivoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioArquivoController"/> class.
        /// </summary>
        /// <param name="arquivoCommands">AvaliacaoCommands interface.</param>
        /// <param name="arquivoQueries">AvaliacaoQueries interface.</param>
        public FuncionarioArquivoController(
            IArquivoCommands arquivoCommands,
            IArquivoQueries arquivoQueries)
        {
            this.arquivoCommands = arquivoCommands;
            this.arquivoQueries = arquivoQueries;
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioArquivoController>/funcionario/2
        [HttpGet("funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetFuncionarioArquivosAsync(int codigoFuncionario)
        {
            var funcionarioArquivos = await arquivoQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(funcionarioArquivos);
        }

        /// <summary>
        /// GetAsync método GetFuncionarioArquivos.
        /// </summary>
        /// <param name="codigoFuncionario">codigoFuncionario.</param>
        /// <param name="tipoDocumento">tipoDocumento.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioArquivoController>/2
        [HttpGet("funcionario/{codigoFuncionario}/tipo/{tipoDocumento}")]
        
        public async Task<IActionResult> GetFuncionarioArquivoAsync(int codigoFuncionario, TipoDocumento tipoDocumento)
        {
            var funcionarioArquivo = await arquivoQueries.BuscarUmAsync(codigoFuncionario, tipoDocumento);

            return Ok(funcionarioArquivo);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioArquivoController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        
        public async Task<IActionResult> InsertArquivoFuncionarioAsync([FromBody] ArquivoModel value)
        {
            var novoArquivoFuncionario = await arquivoCommands.InserirAsync(value);

            return Ok(novoArquivoFuncionario);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método alterar registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<FuncionarioArquivoController>
        [HttpPut]
        
        public async Task<IActionResult> UpdateArquivoFuncionarioAsync([FromBody] ArquivoModel value)
        {
            var alterarArquivoFuncionario = await arquivoCommands.AlterarAsync(value);

            return Ok(alterarArquivoFuncionario);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<FuncionarioArquivoController>/5
        [HttpDelete("{codigo}")]
        
        public async Task<IActionResult> DeleteArquivoFuncionarioAsync(int codigo)
        {
            var arquivoExcluido = await arquivoCommands.ExcluirAsync(codigo);

            return Ok(arquivoExcluido);
        }

        /// <summary>
        /// PostAsync método upload arquivo.
        /// </summary>
        /// <param name="fileModel">fileModel.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioArquivoController>/upload
        [HttpPost("upload")]
        
        public async Task<IActionResult> PostUploadAsync([FromForm] FileModel fileModel)
        {
            var arquivo = await arquivoCommands.UploadAsync(fileModel);

            return Ok(arquivo);
        }

        /// <summary>
        /// PostAsync método download arquivo.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <param name="codigoFuncionario">codigoFuncionario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioArquivoController>/download
        [HttpPost("download/funcionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> PostDownloadAsync([FromBody] string filename, int codigoFuncionario)
        {
            var arquivo = await arquivoCommands.DownloadAsync(filename, codigoFuncionario.ToString());

            return Ok(arquivo);
        }
    }
}
