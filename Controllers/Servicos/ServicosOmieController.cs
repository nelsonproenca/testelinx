using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.Omie.Interfaces;
using RVC.Intranet4.Core.Infrastructure.Omie.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosOmieController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosOmieController : ControllerBase
    {
        private readonly IOmieService omieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosOmieController"/> class.
        /// </summary>
        /// <param name="omieService">OmieService.</param>
        public ServicosOmieController(IOmieService omieService)
        {
            this.omieService = omieService;
        }

        /// <summary>
        /// Consultar Categorias.
        /// </summary>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosOmieController>/empresa/{codigoEmpresa}
        [HttpGet("listarCategorias/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> GetAsync(int codigoEmpresa)
        {
            var enviado = await omieService.ConsultarCategoriasAsync(codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Consultar Cliente.
        /// </summary>
        /// <param name="codigoCliente">codigoCliente.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<ServicosOmieController>/consultarCliente/{codigoCliente}/empresa/{codigoEmpresa}
        [HttpGet("consultarCliente/{codigoCliente}/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> GetAsync(int codigoCliente, int codigoEmpresa)
        {
            ParametrosConsultarCliente parametros = new ParametrosConsultarCliente() 
            {
                CodigoClienteIntegracao = codigoCliente.ToString()
            };

            var enviado = await omieService.ConsultarClienteAsync(parametros, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Consultar Conta Receber.
        /// </summary>
        /// <param name="codigoEmpresa">codigoEmpresa. RVCConsultores = 1| RVCAdvogados = 2.</param>
        /// <param name="codigoLancamentoIntegracao">codigoLancamentoIntegracao.</param>
        /// <param name="codigoLancamentoOmie">codigoLancamentoOmie.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<ServicosOmieController>/consultarContaPagar/empresa/{codigoEmpresa}/lancamento/{codigoLancamentoIntegracao}/{codigoLancamentoOmie}
        [HttpGet("consultarContaPagar/empresa/{codigoEmpresa}/lancamento/{codigoLancamentoIntegracao}/{codigoLancamentoOmie}")]
        
        public async Task<IActionResult> GetConsultarContaPagarAsync(int codigoEmpresa, string codigoLancamentoIntegracao, long codigoLancamentoOmie)
        {
            ParametrosConsultarContas parametros = new ParametrosConsultarContas()
            {
                CodigoLancamentoIntegracao = codigoLancamentoIntegracao,
                CodigoLancamentoOmie = codigoLancamentoOmie
            };

            var enviado = await omieService.ConsultarContaPagarAsync(parametros, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Consultar Conta Receber.
        /// </summary>
        /// <param name="codigoEmpresa">codigoEmpresa. RVCConsultores = 1| RVCAdvogados = 2.</param>
        /// <param name="codigoLancamentoIntegracao">codigoLancamentoIntegracao.</param>
        /// <param name="codigoLancamentoOmie">codigoLancamentoOmie.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<ServicosOmieController>/consultarContaReceber/empresa/{codigoEmpresa}/lancamento/{codigoLancamentoIntegracao}/{codigoLancamentoOmie}
        [HttpGet("consultarContaReceber/empresa/{codigoEmpresa}/lancamento/{codigoLancamentoIntegracao}/{codigoLancamentoOmie}")]
        
        public async Task<IActionResult> GetConsultarContaReceberAsync(int codigoEmpresa, string codigoLancamentoIntegracao, long codigoLancamentoOmie)
        {
            ParametrosConsultarContas parametros = new ParametrosConsultarContas()
            {
                CodigoLancamentoIntegracao = codigoLancamentoIntegracao,
                CodigoLancamentoOmie = codigoLancamentoOmie
            };

            var enviado = await omieService.ConsultarContaReceberAsync(parametros, codigoEmpresa);

            return Ok(enviado);
        }

        /// <summary>
        /// Upsert Conta Receber.
        /// </summary>
        /// <param name="parametros">parametros.</param>
        /// <param name="codigoEmpresa">codigoEmpresa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosOmieController>/upsertContaReceber/empresa/{codigoEmpresa}
        [HttpPost("upsertContaReceber/empresa/{codigoEmpresa}")]
        
        public async Task<IActionResult> PostAsync([FromBody] ParametrosUpsertContaReceber parametros, int codigoEmpresa)
        {
            var enviado = await omieService.UpsertContaReceberAsync(parametros, codigoEmpresa);

            return Ok(enviado);
        }
    }
}
