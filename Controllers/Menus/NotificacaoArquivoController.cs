using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.FileShare.Models;
using RVC.Intranet4.Menus.Application.NotificacaoArquivo.Commands;
using RVC.Intranet4.Menus.Application.NotificacaoArquivo.Models;
using RVC.Intranet4.Menus.Application.NotificacaoArquivo.Queries;

namespace RVC.Intranet4.Web.Controllers.Menus
{
    /// <summary>
    /// NotificacaoArquivoController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class NotificacaoArquivoController : ControllerBase
    {
        private readonly INotificacaoArquivosCommands notificacaoArquivosCommands;
        private readonly INotificacaoArquivoQueries notificacaoArquivoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificacaoArquivoController"/> class.
        /// </summary>
        /// <param name="notificacaoArquivosCommands">NotificacaoArquivosCommands interface.</param>
        /// <param name="notificacaoArquivoQueries">NotificacaoArquivoQueries interface.</param>
        public NotificacaoArquivoController(
            INotificacaoArquivosCommands notificacaoArquivosCommands,
            INotificacaoArquivoQueries notificacaoArquivoQueries)
        {
            this.notificacaoArquivosCommands = notificacaoArquivosCommands;
            this.notificacaoArquivoQueries = notificacaoArquivoQueries;
        }

        /// <summary>
        /// GetAsync método Por Notificacao.
        /// </summary>
        /// <param name="codigoNotificacao">código do Notificacao.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<NotificacaoArquivoController>/notificacao/2
        [HttpGet("notificacao/{codigoNotificacao}")]
        public async Task<IActionResult> GetPorNotificacaoAsync(int codigoNotificacao)
        {
            var notificacaoArquivos = await notificacaoArquivoQueries.BuscarPorNotificacaoAsync(codigoNotificacao);

            return Ok(notificacaoArquivos);
        }

        /// <summary>
        /// GetAsync método GetNotificacaoArquivo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<NotificacaoArquivoController>/2
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetNotificacaoArquivoAsync(int codigo)
        {
            var arquivo = await notificacaoArquivoQueries.BuscarUmAsync(codigo);

            return Ok(arquivo);
        }

        /// <summary>
        /// PostAsync método download arquivo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoArquivoController>/notificacao/{codigoNotificacao}/download
        [HttpGet("{codigo}/download")]
        public async Task<IActionResult> PostDownloadAsync(int codigo)
        {
            var arquivo = await notificacaoArquivoQueries.DownloadAsync(codigo);

            return Ok(arquivo);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<NotificacaoArquivoController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> InsertNotificacaoArquivoAsync([FromBody] NotificacaoArquivoModel value)
        {
            var novoNotificacaoArquivo = await notificacaoArquivosCommands.InserirAsync(value);

            return Ok(novoNotificacaoArquivo);
        }

        /// <summary>
        /// UpdateAsync método alterar registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoArquivoController>
        [HttpPut("{codigo}")]
        public async Task<IActionResult> UpdateNotificacaoArquivoAsync([FromBody] NotificacaoArquivoModel value, int codigo)
        {
            var alterarNotificacaoArquivo = await notificacaoArquivosCommands.AlterarAsync(value, codigo);

            return Ok(alterarNotificacaoArquivo);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<NotificacaoArquivoController>/5
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteNotificacaoArquivoAsync(int codigo)
        {
            var arquivoExcluido = await notificacaoArquivosCommands.ExcluirAsync(codigo);

            return Ok(arquivoExcluido);
        }

        /// <summary>
        /// PostAsync método upload arquivo.
        /// </summary>
        /// <param name="fileModel">fileModel.</param>
        /// <param name="codigoNotificacao">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<NotificacaoArquivoController>/upload
        [HttpPost("notificacao/{codigoNotificacao}/upload")]
        public async Task<IActionResult> PostUploadAsync([FromForm] FileModel fileModel, int codigoNotificacao)
        {
            var arquivo = await notificacaoArquivosCommands.UploadAsync(fileModel, codigoNotificacao);

            return Ok(arquivo);
        }
    }
}