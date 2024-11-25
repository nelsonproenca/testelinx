using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Enums;
using RVC.Intranet4.Core.Infrastructure.FileShare.Models;
using RVC.Intranet4.Core.Infrastructure.IntegracaoBancaria.Cnab240.Interfaces;
using RVC.Intranet4.Core.Infrastructure.IntegracaoBancaria.Cnab240.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosCnab240Controller : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosCnab240Controller : ControllerBase
    {
        private readonly ICnab240Service cnab240Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosCnab240Controller"/> class.
        /// </summary>
        /// <param name="cnab240Service">Cnab240Service.</param>
        public ServicosCnab240Controller(ICnab240Service cnab240Service)
        {
            this.cnab240Service = cnab240Service;
        }

        /// <summary>
        /// Enviar Cnab240 (Async).
        /// </summary>
        /// <param name="value">entidade.</param>
        /// <param name="tipoIntegracao">tipoIntegracao.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosCnab240Controller>/
        [HttpPost("integracao/{tipoIntegracao}")]
        public async Task<IActionResult> PostAsync([FromBody] Cnab240Entry value, TipoIntegracao tipoIntegracao)
        {
            var enviado = await cnab240Service.GerarAsync(value.Pagador, value.Favorecidos, tipoIntegracao);

            return Ok(enviado);
        }

        /// <summary>
        /// PostAsync método download arquivo.
        /// </summary>
        /// <param name="filename">filename.</param>
        /// <param name="containerName">containerName.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosCnab240Controller>/download
        [HttpPost("download/container/{containerName}")]
        
        public async Task<IActionResult> PostDownloadAsync([FromBody] string filename, string containerName)
        {
            var arquivo = await cnab240Service.DownloadAsync(filename, containerName);

            return Ok(arquivo);
        }

        /// <summary>
        /// Buscar arquivos remessa.
        /// </summary>
        /// <param name="containerName">containerName.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<ServicosCnab240Controller>
        [HttpGet("container/{containerName}")]
        
        public async Task<IActionResult> GetAsync(string containerName)
        {
            var comprovantes = await cnab240Service.BuscarArquivosAsync(containerName);

            return Ok(comprovantes);
        }

        /// <summary>
        /// PostAsync método upload arquivo csv.
        /// </summary>
        /// <param name="fileModel">fileModel.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<AvaliacaoController>/5
        [HttpPost("upload")]
        
        public async Task<IActionResult> PostUploadAsync([FromForm] FileModel fileModel)
        {
            var arquivo = await cnab240Service.UploadAsync(fileModel);

            return Ok(arquivo);
        }
    }
}
