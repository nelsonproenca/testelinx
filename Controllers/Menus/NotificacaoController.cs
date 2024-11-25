using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Menus.Application.Notificacao.Commands;
using RVC.Intranet4.Menus.Application.Notificacao.Models;
using RVC.Intranet4.Menus.Application.Notificacao.Queries;
using RVC.Intranet4.Shareds.Application;

namespace RVC.Intranet4.Web.Controllers.Menus
{
    /// <summary>
    /// Notificacao Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoCommands notificacaoCommands;
        private readonly INotificacaoQueries notificacaoQueries;
        private readonly IFunctions functions;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificacaoController"/> class.
        /// </summary>
        /// <param name="notificacaoCommands">notificacaoCommands interface.</param>
        /// <param name="notificacaoQueries">notificacaoQueries interface.</param>
        /// <param name="functions">functions.</param>
        public NotificacaoController(
            INotificacaoCommands notificacaoCommands, 
            INotificacaoQueries notificacaoQueries,
            IFunctions functions)
        {
            this.notificacaoCommands = notificacaoCommands;
            this.notificacaoQueries = notificacaoQueries;
            this.functions = functions;
        }

        /// <summary>
        /// GetAsync método BuscarTodos.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<NotificacaoController>
        [HttpGet]
        
        public async Task<IActionResult> GetAsync()
        {
            var notificacoes = await notificacaoQueries.BuscarTodosAsync();

            return Ok(notificacoes);
        }

        /// <summary>
        /// GetAsync método BuscarUm por codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoController>/5
        [HttpGet("{codigo}")]
        
        public async Task<IActionResult> GetPorCodigoAsync(int codigo)
        {
            var notificacao = await notificacaoQueries.BuscarUmAsync(codigo);

            return Ok(notificacao);
        }

        /// <summary>
        /// GetPorTipoAsync método BuscarTodos por tipo de notificação.
        /// </summary>
        /// <param name="tipoNotificacao">tipo da notificação.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<NotificacaoController>
        [HttpGet("porTipo/{tipoNotificacao}")]
        
        public async Task<IActionResult> GetPorTipoAsync(int tipoNotificacao)
        {
            var notificacoes = await notificacaoQueries.BuscarPorTipoAsync(tipoNotificacao);

            return Ok(notificacoes);
        }

        /// <summary>
        /// GetPorFuncionarioAsync método BuscarUm por codigoFuncionario.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoController>/5
        [HttpGet("porFuncionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetPorFuncionarioAsync(int codigoFuncionario)
        {
            var notificacao = await notificacaoQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(notificacao);
        }

        /// <summary>
        /// GetListaPopupAsync método notificações por codigoFuncionario.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoController>/5
        [HttpGet("listaPopup/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetListaPopupAsync(int codigoFuncionario)
        {
            var notificacao = await notificacaoQueries.BuscarPorListaPopupAsync(codigoFuncionario);

            return Ok(notificacao);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<NotificacaoController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        
        public async Task<IActionResult> PostAsync([FromBody] NotificacaoModel value)
        {
            var novaEntidade = await notificacaoCommands.InserirAsync(value);

            return Ok(novaEntidade);
        }

        /// <summary>
        /// PutAsync método Alterar um registro.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoController>/5
        [HttpPut("{codigo}")]
        
        public async Task<IActionResult> PutAsync(int codigo, [FromBody] NotificacaoModel value)
        {
            var entidadeAlterada = await notificacaoCommands.AlterarAsync(value, codigo);

            return Ok(entidadeAlterada);
        }

        /// <summary>
        /// PutAsync método Alterar um registro.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoController>/5
        [HttpPut("monitorarLeitura/{codigo}")]
        
        public async Task<IActionResult> PutOneAsync(int codigo, [FromBody] bool value)
        {
            var entidadeAlterada = await notificacaoCommands.AlterarUmAsync(value, codigo);

            return Ok(entidadeAlterada);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<NotificacaoController>/5
        [HttpDelete("{codigo}")]
        
        public async Task<IActionResult> DeleteAsync(int codigo)
        {
            var entidadeExcluida = await notificacaoCommands.ExcluirAsync(codigo);

            return Ok(entidadeExcluida);
        }

        /// <summary>
        /// PostAsync método Reenviar um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<NotificacaoController>/5
        [HttpPost("reenviar/{codigo}")]
        
        public async Task<IActionResult> PostReenviarAsync(int codigo)
        {
            var entidade = await notificacaoCommands.ReenviarAsync(codigo);

            return Ok(entidade);
        }

        /// <summary>
        /// PostAsync método Reenviar um registro pelo codigo.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoController>/recript/{email}
        [HttpPut("recript/{email}")]
        
        public async Task<IActionResult> PostReenviarAsync(string email)
        {
            var entidade = await functions.BuscarMensagensFuncionarioUsuarioMasterAsync(email);

            return Ok(entidade);
        }
    }
}
