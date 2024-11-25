using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.Criptografia.Interfaces;
using RVC.Intranet4.Core.Infrastructure.Criptografia.Models;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ServicosCriptografiaController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosCriptografiaController : ControllerBase
    {
        private readonly ICryptService cryptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosCriptografiaController"/> class.
        /// </summary>
        /// <param name="cryptService">cryptService.</param>
        public ServicosCriptografiaController(ICryptService cryptService)
        {
            this.cryptService = cryptService;
        }

        /// <summary>
        /// Desencripta o dado solicitado.
        /// </summary>
        /// <param name="value">CryptParamsModel.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosCriptografiaController>/decrypt
        [HttpPost("decrypt")]
        
        public IActionResult PostDecryptAsync([FromBody] CryptParamsModel value)
        {
            var enviado = cryptService.Decrypt(value);

            return Ok(enviado);
        }

        /// <summary>
        /// Encripta o dado solicitado.
        /// </summary>
        /// <param name="value">CryptParamsModel.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosCriptografiaController>/encrypt
        [HttpPost("encrypt")]
        
        public IActionResult PostEncryptAsync([FromBody] CryptParamsModel value)
        {
            var enviado = cryptService.Encrypt(value);

            return Ok(enviado);
        }
    }
}
