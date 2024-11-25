using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Core.Infrastructure.ChatGpt.Interfaces;

namespace RVC.Intranet4.Web.Controllers.Servicos
{
    /// <summary>
    /// ChatGptController : ControllerBase.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class ServicosChatGptController : ControllerBase
    {
        private readonly IChatGptService chatGptService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicosChatGptController"/> class.
        /// </summary>
        /// <param name="chatGptService">chatGptService.</param>
        public ServicosChatGptController(IChatGptService chatGptService)
        {
            this.chatGptService = chatGptService;
        }

        /// <summary>
        /// ChatGpt - Fazer uma pergunta ao chatGpt.
        /// </summary>
        /// <param name="value">valor de pesquisa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosChatGptController>/conversation
        [HttpPost("conversation")]
        
        public async Task<IActionResult> GetConversationAsync([FromBody] string value)
        {
            var enviado = await chatGptService.ConversationAsync(value);

            return Ok(enviado);
        }

        /// <summary>
        /// ChatGpt - Fazer a Analise de sentimento.
        /// </summary>
        /// <param name="value">valor de pesquisa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosChatGptController>/sentiment
        [HttpPost("sentiment")]
        
        public async Task<IActionResult> GetSentimentAsync([FromBody] string value)
        {
            var enviado = await chatGptService.DetectingSentimentAsync(value);

            return Ok(enviado);
        }

        /// <summary>
        /// ChatGpt - Fazer resumo de um texto.
        /// </summary>
        /// <param name="value">valor de pesquisa.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<ServicosChatGptController>/summarize
        [HttpPost("summarize")]
        
        public async Task<IActionResult> GetSummarizeAsync([FromBody] string value)
        {
            var enviado = await chatGptService.SummarizeAsync(value);

            return Ok(enviado);
        }
    }
}
