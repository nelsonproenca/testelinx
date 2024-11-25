using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.NFSe.Interfaces;
using RVC.Intranet4.Core.Infrastructure.NFSe.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosNFSeController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosNFSeController : ControllerBase
    {
        private readonly INFSeService nfSeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosNFSeController"/> class.
        /// </summary>
        /// <param name="nfSeService">nfSeService.</param>
        public ServicosNFSeController(INFSeService nfSeService)
        {
            this.nfSeService = nfSeService;
        }

        /// <summary>
        /// Consultar NFSe.
        /// </summary>
        /// <param name="referencia">referencia.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<ServicosNFSeController>/
        [HttpGet("consultar/referencia/{referencia}/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> GetAsync(string referencia, int codigoEmpresa)
        {
            var enviado = await nfSeService.Consultar(referencia, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Autorizar NFSe.
        /// </summary>
        /// <param name="nfSeModel">nfSeModel.</param>
        /// <param name="referencia">referencia.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosNFSeController>/5
        [HttpPost("autorizar/referencia/{referencia}/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> PostAsync([FromBody] NFSeModel nfSeModel, string referencia, int codigoEmpresa)
        {
            var enviado = await nfSeService.Autorizar(referencia, nfSeModel, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Cancelar NFSe.
        /// </summary>
        /// <param name="referencia">referencia.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <param name="justificativa">justificativa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosNFSeController>/5
        [HttpDelete("cancelar/referencia/{referencia}/empresa/{codigoEmpresa}/{justificativa}")]
        
        public async Task<IActionResult> DeleteAsync(string referencia, int codigoEmpresa, string justificativa)
        {
            var enviado = await nfSeService.Cancelar(referencia, justificativa, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Enviar NFSe por e-mail.
        /// </summary>
        /// <param name="emails">lista e-mails.</param>
        /// <param name="referencia">referencia.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosNFSeController>/5
        [HttpPost("enviarEmail/referencia/{referencia}/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> PostEnviarEmailAsync([FromBody] ListaEmailsModel emails, string referencia, int codigoEmpresa)
        {
            var enviado = await nfSeService.EnviarEmail(referencia, emails, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Excluir Hooks.
        /// </summary>
        /// <param name="hookId">hookId.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosNFSeController>/5
        [HttpDelete("hooks/{hookId}/excluir/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> DeleteExcluirHookAsync(string hookId, int codigoEmpresa)
        {
            var enviado = await nfSeService.ExcluirHooks(hookId, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Criar Hook.
        /// </summary>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <param name="cnpj">cnpj.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosNFSeController>/5
        [HttpPost("hooks/empresa/{codigoEmpresa}/cnpj/{cnpj}/criar")]
        
        public async Task<IActionResult> PostCriarHookAsync(int codigoEmpresa, string cnpj)
        {
            var enviado = await nfSeService.CriarHook(codigoEmpresa, cnpj);

            return Ok(enviado);
        }

        /// <summary>
        /// Consultar Hook.
        /// </summary>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<ServicosNFSeController>/
        [HttpGet("hooks/consultar")]
        
        public async Task<IActionResult> GetConsultarHooksAsync(int codigoEmpresa)
        {
            var enviado = await nfSeService.ConsultarHooks(codigoEmpresa);

            return Ok(enviado);
        }
    }
}
