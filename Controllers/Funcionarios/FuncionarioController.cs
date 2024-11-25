using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Commands;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Models;
using RVC.Intranet4.Funcionarios.Application.Funcionario.Queries;

namespace RVC.Intranet4.Web.Controllers.Funcionarios
{
    /// <summary>
    /// FuncionarioController : ControllerBase.
    /// </summary>  
    [Route("v1/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioCommands funcionarioCommands;
        private readonly IFuncionarioQueries funcionarioQueries;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncionarioController"/> class.
        /// </summary>
        /// <param name="funcionarioCommands">FuncionarioCommands interface.</param>
        /// <param name="funcionarioQueries">FuncionarioQueries interface.</param>
        public FuncionarioController(
            IFuncionarioCommands funcionarioCommands,
            IFuncionarioQueries funcionarioQueries)
        {
            this.funcionarioCommands = funcionarioCommands;
            this.funcionarioQueries = funcionarioQueries;
        }

        /// <summary>
        /// GetAsync método BuscarTodos com paginação.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>
        [HttpGet]
        public async Task<IActionResult> GetFuncionariosAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarTodosAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método GetFuncionario.
        /// </summary>
        /// <param name="codigo">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetFuncionarioAsync(int codigo)
        {
            var funcionario = await funcionarioQueries.BuscarUmAsync(codigo);

            return Ok(funcionario);
        }

        /// <summary>
        /// GetAsync método GetDadosPessoais.
        /// </summary>
        /// <param name="codigoFuncionario">código do funcionário.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>
        [HttpGet("dadosPessoais/{codigoFuncionario}")]
        public async Task<IActionResult> GetDadosPessoaisAsync(int codigoFuncionario)
        {
            var funcionario = await funcionarioQueries.BuscarDadosPessoaisAsync(codigoFuncionario);

            return Ok(funcionario);
        }

        /// <summary>
        /// GetAsync método BuscarAtivosAsync.
        /// </summary>
        /// <param name="codigoFuncionario">codigo Funcionario.</param>
        /// <param name="mostrarSocios">mostrarSocios.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/ativos/5
        [HttpGet("ativos/{codigoFuncionario}/socios/{mostrarSocios}")]
        public async Task<IActionResult> GetAtivosAsync(int codigoFuncionario, bool mostrarSocios)
        {
            var funcionarios = await funcionarioQueries.BuscarAtivosAsync(codigoFuncionario, mostrarSocios);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Todos Socios Ativos.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/sociosAtivos
        [HttpGet("sociosAtivos")]
        public async Task<IActionResult> BuscarTodosSociosAtivosAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarTodosSociosAtivosAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Socios Pipeline sem as areas (Administrativo, TaxIt, TaxTransformation).
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/sociosPipeline
        [HttpGet("sociosPipeline")]
        public async Task<IActionResult> GetSociosPipelineAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarSociosPipelineAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Socios TI Pipeline.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/sociosti
        [HttpGet("sociosti")]
        public async Task<IActionResult> GetSociosTIPipelineAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarSociosTIPipelineAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Gerentes Pipeline.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/gerentes
        [HttpGet("gerentes")]
        public async Task<IActionResult> GetGerentesPipelineAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarGerentesPipelineAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Gerentes Pipeline.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/advogadosr
        [HttpGet("advogadosr")]
        public async Task<IActionResult> GetAdvogadoSrPipelineAsync()
        {
            var funcionarios = await funcionarioQueries.BuscarAdvogadoSrPipelineAsync();

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetPorPerfilAsync método BuscarPorPerfilAsync.
        /// </summary>
        /// <param name="perfil">perfil.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/5
        [HttpGet("porPerfil/{perfil}")]
        public async Task<IActionResult> GetPorPerfilAsync(string perfil)
        {
            var funcionarios = await funcionarioQueries.BuscarPorPerfilAsync(perfil);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetPorApelidoAsync método BuscarPorApelidoAsync.
        /// </summary>
        /// <param name="apelido">apelido.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/porApelido/Nelson Proença
        [HttpGet("porApelido/{apelido}")]
        public async Task<IActionResult> GetPorApelidoAsync(string apelido)
        {
            var funcionarios = await funcionarioQueries.BuscarPorApelidoAsync(apelido);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método BuscarPorCoachAsync com paginação.
        /// </summary>
        /// <param name="codigoCoach">codigo Coach.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/porCoach/5
        [HttpGet("porCoach/{codigoCoach}")]
        public async Task<IActionResult> GetPorCoachAsync(int codigoCoach)
        {
            var funcionarios = await funcionarioQueries.BuscarPorCoachAsync(codigoCoach);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método BuscarPorLiderAsync com paginação.
        /// </summary>
        /// <param name="codigoLider">codigo Lider.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/porLider/5
        [HttpGet("porLider/{codigoLider}")]
        public async Task<IActionResult> GetPorLiderAsync(int codigoLider)
        {
            var funcionarios = await funcionarioQueries.BuscarPorLiderAsync(codigoLider);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método BuscarPorNomeAsync.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("nomes")]
        public async Task<IActionResult> GetPorNomeAsync()
        {
            var nomes = await funcionarioQueries.BuscarNomesAsync();

            return Ok(nomes);
        }

        /// <summary>
        /// Get - Buscar profissional por email (RPA).
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/porLider/5
        [HttpGet("rpa/email/{email}")]
        public async Task<IActionResult> GetPorEmailAsync(string email)
        {
            var profissional = await funcionarioQueries.BuscarPorEmail2Async(email);

            return Ok(profissional ?? new FuncionarioRPAModel());
        }

        /// <summary>
        /// Get - Buscar profissional por email (RPA).
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/porLider/5
        [HttpGet("rpa/profissional/{email}")]
        public async Task<IActionResult> GetProfissionaisPorEmailAsync(string email)
        {
            var profissional = await funcionarioQueries.BuscarPorEmailAsync(email);

            return Ok(profissional);
        }

        /// <summary>
        /// Get - Buscar funcionarios CoachCoachees (RPA).
        /// </summary>
        /// <param name="email">email.</param>
        /// <param name="nomeCoachee">nome do Coach.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/rpa/coachCoachees
        [HttpGet("rpa/coachCoachees/email/")]
        public async Task<IActionResult> GetCoachCoacheesAsync(string email = "", string nomeCoachee = "")
        {
            var funcionarios = await funcionarioQueries.BuscarCoachCoacheesAsync(email, nomeCoachee);

            return Ok(funcionarios);
        }

        /// <summary>
        /// Get - Buscar funcionarios CoachCoachees (RPA).
        /// </summary>
        /// <param name="codigoProjeto">codigoProjeto.</param>
        /// <param name="nomeProjeto">nomeProjeto.</param>
        /// <param name="nomeCliente">nomeCliente.</param>
        /// <param name="status">status.</param>
        /// <param name="nomeFuncionario">nomeFuncionario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/rpa/projetosSocio
        [HttpGet("rpa/projetosSocio/filtro")]
        public async Task<IActionResult> GetProjetosSocioAsync(string codigoProjeto = "", string nomeProjeto = "", string nomeCliente = "", int status = 0, string nomeFuncionario = "")
        {
            var funcionarios = await funcionarioQueries.BuscarProjetosSocioAsync(codigoProjeto, nomeProjeto, nomeCliente, status, nomeFuncionario);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Saldo Ferias.
        /// </summary>
        /// <param name="codigoArea">codigoArea.</param>
        /// <param name="codigoLider">codigoLider.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/saldoFerias
        [HttpGet("saldoFerias")]
        public async Task<IActionResult> GetSaldoFerias(int codigoArea, int codigoLider)
        {
            var funcionarios = await funcionarioQueries.BuscarSaldoFerias(codigoArea, codigoLider);

            return Ok(funcionarios);
        }

        /// <summary>
        /// GetAsync método Buscar Saldo Ferias por Funcionario.
        /// </summary>
        /// <param name="codigoFuncionario">codigoFuncionario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // GET: api/<FuncionarioController>/saldoFeriasFuncionario
        [HttpGet("saldoFerias/funcionario/{codigoFuncionario}")]
        public async Task<IActionResult> GetSaldoFeriasFuncionario(int codigoFuncionario)
        {
            var funcionarios = await funcionarioQueries.BuscarSaldoFeriasFuncionario(codigoFuncionario);

            return Ok(funcionarios);
        }

        /// <summary>
        /// PostAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioController>
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] FuncionarioModel value)
        {
            var novoFuncionario = await funcionarioCommands.InserirAsync(value);

            return Ok(novoFuncionario);
        }

        /// <summary>
        /// UpdateFuncionarioAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // POST api/<FuncionarioController>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] FuncionarioModel value)
        {
            var funcionarioAlterado = await funcionarioCommands.AlterarAsync(value);

            return Ok(funcionarioAlterado);
        }

        /// <summary>
        /// DeleteAsync método Excluir um registro pelo codigo.
        /// </summary>
        /// <param name="codigo">codigo.</param>
        /// <param name="motivoDesligamento">motivo desligamento.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{codigo}/{motivoDesligamento}")]
        public async Task<IActionResult> DeleteAsync(int codigo, string motivoDesligamento)
        {
            var funcionarioExcluido = await funcionarioCommands.ExcluirAsync(codigo, motivoDesligamento);

            return Ok(funcionarioExcluido);
        }

        /// <summary>
        /// UpdateFuncionarioDadosPessoaisAsync método Inserir novo registro.
        /// </summary>
        /// <param name="value">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        // PUT api/<FuncionarioController>/dadosPessoais
        [HttpPut("dadosPessoais")]
        public async Task<IActionResult> UpdateDadosPessoaisAsync([FromBody] DadosPessoaisModel value)
        {
            var alterarFuncionario = await funcionarioCommands.AlterarDadosPessoaisAsync(value);

            return Ok(alterarFuncionario);
        }
    }
}