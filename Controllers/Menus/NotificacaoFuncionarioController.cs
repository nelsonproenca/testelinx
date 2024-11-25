using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Menus.Application.NotificacaoFuncionario.Commands;
using RVC.Intranet4.Menus.Application.NotificacaoFuncionario.Models;
using RVC.Intranet4.Menus.Application.NotificacaoFuncionario.Queries;

namespace RVC.Intranet4.Web.Controllers.Menus
{
    /// <summary>
    /// NotificacaoFuncionario Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class NotificacaoFuncionarioController : ControllerBase
    {
        private readonly INotificacaoFuncionarioCommands notificacaoCommands;
        private readonly INotificacaoFuncionarioQueries notificacaoQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificacaoFuncionarioController"/> class.
        /// </summary>
        /// <param name="notificacaoCommands">notificacaoCommands interface.</param>
        /// <param name="notificacaoQueries">notificacaoQueries interface.</param>
        public NotificacaoFuncionarioController(INotificacaoFuncionarioCommands notificacaoCommands, INotificacaoFuncionarioQueries notificacaoQueries)
        {
            this.notificacaoCommands = notificacaoCommands;
            this.notificacaoQueries = notificacaoQueries;
        }

        /// <summary>
        /// GetAsync método BuscarTodos.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<NotificacaoFuncionarioController>
        [HttpGet]
        
        public async Task<IActionResult> GetAsync()
        {
            var notificacoes = await notificacaoQueries.BuscarTodosAsync();

            return Ok(notificacoes);
        }

        /// <summary>
        /// GetAsync método BuscarUm por id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoFuncionarioController>/5
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetPorCodigoAsync(int id)
        {
            var notificacao = await notificacaoQueries.BuscarUmAsync(id);

            return Ok(notificacao);
        }

        /// <summary>
        /// GetPorFuncionarioAsync método BuscarUm por codigoFuncionario.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET api/<NotificacaoFuncionarioController>/5
        [HttpGet("porFuncionario/{codigoFuncionario}")]
        
        public async Task<IActionResult> GetPorFuncionarioAsync(int codigoFuncionario)
        {
            var notificacao = await notificacaoQueries.BuscarPorFuncionarioAsync(codigoFuncionario);

            return Ok(notificacao);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<NotificacaoFuncionarioController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        
        public async Task<IActionResult> PostAsync([FromBody] NotificacaoFuncionarioModel value)
        {
            var novaEntidade = await notificacaoCommands.InserirAsync(value);

            return Ok(novaEntidade);
        }

        /// <summary>
        /// PutAsync método Alterar um registro.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="value">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoFuncionarioController>/5
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutAsync(int id, [FromBody] NotificacaoFuncionarioModel value)
        {
            var entidadeAlterada = await notificacaoCommands.AlterarAsync(value, id);

            return Ok(entidadeAlterada);
        }

        /// <summary>
        /// PutUmAsync método Alterar um item do registro.
        /// </summary>
        /// <param name="id">id.</param>
        /// <param name="status">entidade para alterar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<NotificacaoFuncionarioController>/5
        [HttpPut("Status/{id}")]
        
        public async Task<IActionResult> PutUmAsync(int id, int status)
        {
            var entidadeAlterada = await notificacaoCommands.AlterarUmAsync(status, id);

            return Ok(entidadeAlterada);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<NotificacaoFuncionarioController>/5
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var entidadeExcluida = await notificacaoCommands.ExcluirAsync(id);

            return Ok(entidadeExcluida);
        }

        /// <summary>
        /// DeleteTodosAsync método Excluir todos os registro pelo id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<NotificacaoFuncionarioController>/5
        [HttpDelete("all/{id}")]
        
        public async Task<IActionResult> DeleteTodosAsync(int id)
        {
            var entidadeExcluida = await notificacaoCommands.ExcluirTodosAsync(id);

            return Ok(entidadeExcluida);
        }
    }
}
