using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RVC.Intranet4.Autenticacao.Application.Login.Models;
using RVC.Intranet4.Autenticacao.Application.Perfil.Commands;
using RVC.Intranet4.Autenticacao.Application.Perfil.Models;
using RVC.Intranet4.Autenticacao.Application.Perfil.Queries;
using RVC.Intranet4.Autenticacao.Application.Queries;
using RVC.Intranet4.Autenticacao.Application.Usuario.Commands;
using RVC.Intranet4.Autenticacao.Application.Usuario.Models;
using RVC.Intranet4.Core.Settings;
using RVC.Intranet4.Shareds.Application;

namespace RVC.Intranet4.Web.Controllers.Autenticacao
{
    /// <summary>
    /// Autenticacao Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly ILoginQueries loginQueries;
        private readonly IUsuarioCommands usuarioCommands;
        private readonly IPerfilCommands perfilCommands;
        private readonly IPerfilQueries perfilQueries;
        private readonly IFunctions functions;
        private readonly IdentityServerSettings identityServerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutenticacaoController"/> class.
        /// </summary>
        /// <param name="loginQueries">loginQueries.</param>
        /// <param name="usuarioCommands">usuarioCommands.</param>
        /// <param name="perfilCommands">perfilCommands.</param>
        /// <param name="perfilQueries">perfilQueries.</param>
        /// <param name="configuration">configuration.</param>
        /// <param name="functions">functions.</param>
        public AutenticacaoController(
            ILoginQueries loginQueries,
            IUsuarioCommands usuarioCommands,
            IPerfilCommands perfilCommands,
            IPerfilQueries perfilQueries,
            IConfiguration configuration,
            IFunctions functions)
        {
            this.perfilCommands = perfilCommands;
            this.perfilQueries = perfilQueries;
            this.functions = functions;
            this.usuarioCommands = usuarioCommands;
            this.loginQueries = loginQueries;
            identityServerSettings = configuration.GetSection("IdentityServerSettings").Get<IdentityServerSettings>();
        }

        /// <summary>
        /// Login método Autenticação usuário.
        /// </summary>
        /// <param name="loginUser">Dados usuário.</param>       
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("loginUsuario")]
        public async Task<IActionResult> Login(LoginUserModel loginUser)
        {
            var user = await loginQueries.LoginAsync(loginUser);

            return Ok(user);
        }

        /// <summary>
        /// Obtem o Perfil do usuário pelo e-mail.
        /// </summary>
        /// <param name="email">email usuário.</param>       
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("perfil/email/{email}")]
        public async Task<IActionResult> PerfilAsync(string email)
        {
            var perfil = await loginQueries.GetPerfilAsync(email);

            return Ok(perfil);
        }

        /// <summary>
        /// PostAsync método criar novo usuário.
        /// </summary>
        /// <param name="criarUsuario">criarUsuario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("criarUsuario")]
        [ProducesErrorResponseType(typeof(string))]
        public async Task<IActionResult> CriarUsuarioAsync([FromBody] CriarUsuarioModel criarUsuario)
        {
            var novoUsuario = await usuarioCommands.CriarUsuarioAsync(criarUsuario);

            if (novoUsuario.Mensagem.Length == 0)
            {
                return Ok(new { Code = "Success", Description = $"Usuário criado com sucesso. E-mail enviado com a senha de acesso e o token de confirmação." });
            }
            else
            {
                return Ok(new { Code = "Error", Description = novoUsuario.Mensagem });
            }
        }

        /// <summary>
        /// AlterarSenhaUsuarioAsync método alterara senha do usuário.
        /// </summary>
        /// <param name="alterarSenhaUsuario">alterarSenhaUsuario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("alterarSenhaUsuario")]
        public async Task<IActionResult> AlterarSenhaUsuarioAsync([FromBody] AlterarSenhaUsuarioModel alterarSenhaUsuario)
        {
            var novoUsuario = await usuarioCommands.AlterarSenhaUsuarioAsync(alterarSenhaUsuario);

            if (novoUsuario == null)
            {
                return Ok(new { Code = "Error", Description = "Usuário não cadastrado." });
            }

            if (novoUsuario.Succeeded)
            {
                return Ok(new { Code = "Success", Description = "Senha alterada com sucesso" });
            }

            return Ok(novoUsuario.Errors);
        }

        /// <summary>
        /// AlterarSenhaNovoAsync método alterara senha do usuário.
        /// </summary>
        /// <param name="alterarSenhaUsuario">alterarSenhaUsuario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("alterarSenhaNovo")]
        public async Task<IActionResult> AlterarSenhaNovoAsync([FromBody] AlterarSenhaUsuarioModel alterarSenhaUsuario)
        {
            try
            {
                var result = await functions.CriptografarMensagensUsuarioAsync(alterarSenhaUsuario);

                if (string.IsNullOrEmpty(result))
                {
                    return Ok(new
                    {
                        Code = "Success",
                        Description = "Criptografia de Mensagens executada com sucesso."
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Code = "Error",
                        Description = "Erro ao Criptografia de Mensagens do Usuário."
                    });
                }
            }
            catch (System.Exception ex)
            {
                return Ok(new
                {
                    Code = "Error",
                    Description = ex.Message
                });
            }
        }

        /// <summary>
        /// PostAsync método adicionar permissoes ao usuário.
        /// </summary>
        /// <param name="model">nova entidade para criar.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("adicionarPermissoesUsuario")]
        public async Task<IActionResult> AdicionarPermissoesAsync([FromBody] PerfilUsuarioModel model)
        {
            var novoPerfil = await perfilCommands.AdicionarPerfilAsync(model);

            if (novoPerfil)
            {
                return Ok(new { Code = "Success", Description = "Permissões criadas com sucesso" });
            }
            else
            {
                return Ok(new { Code = "Error", Description = "Erro ao cadastrar permissões" });
            }
        }

        /// <summary>
        /// PostAsync método remover perfil do usuário.
        /// </summary>
        /// <param name="novoPerfil">novoPerfil.</param>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPut("removerPerfil/{email}")]
        public async Task<IActionResult> AdicionarPermissoesAsync([FromBody] string novoPerfil, string email)
        {
            var perfil = await perfilCommands.RemoverPerfilAsync(novoPerfil, email);

            if (perfil)
            {
                return Ok(new { Code = "Success", Description = "Perfil removido com sucesso." });
            }
            else
            {
                return Ok(new { Code = "Error", Description = "Erro ao remover perfil." });
            }
        }

        /// <summary>
        /// GetAsync método obter a senha padrão.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("senhaDefault")]
        public IActionResult GetSenhaDefaultAsync() => Ok(identityServerSettings.PasswordDefault);

        /// <summary>
        /// PostAsync método gera token para reset senha usuário.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("esquecerSenhaUsuario/{email}")]
        public async Task<IActionResult> EsquecerSenhaUsuarioAsync(string email)
        {
            var result = await usuarioCommands.EsquecerSenhaUsuarioAsync(email);

            if (result != (int)HttpStatusCode.Accepted)
            {
                return Ok(new { Code = "Error", Description = "Não foi possivel enviar e-mail com o token de reset de senha.\rTente novamente mais tarde." });
            }
            else
            {
                return Ok(new { Code = "Success", Description = "E-mail enviado com o token de reset de senha." });
            }
        }

        /// <summary>
        /// ResetarSenhaUsuarioAsync método para adicionar uma nova senha ao usuário.
        /// </summary>
        /// <param name="resetarSenhaUsuario">resetarSenhaUsuario.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("resetarSenhaUsuario")]
        public async Task<IActionResult> ResetarSenhaUsuarioAsync([FromBody] ResetarSenhaUsuarioModel resetarSenhaUsuario)
        {
            var novoUsuario = await usuarioCommands.ResetarSenhaUsuarioAsync(resetarSenhaUsuario);

            if (novoUsuario == null)
            {
                return Ok(new { Code = "Error", Description = "Não foi possível resetar a senha no app." });
            }

            if (novoUsuario.Succeeded)
            {
                return Ok(new { Code = "Success", Description = "Senha do Usuário alterarda com sucesso" });
            }

            return Ok(novoUsuario.Errors);
        }

        /// <summary>
        /// ConfirmarEmailUsuarioAsync método para confirmar o email do usuário.
        /// </summary>
        /// <param name="token">token.</param>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("confirmarEmailUsuario/{email}")]
        public async Task<IActionResult> ConfirmarEmailUsuarioAsync([FromBody] string token, string email)
        {
            var novoUsuario = await usuarioCommands.ConfirmarEmailUsuarioAsync(email, token);

            if (novoUsuario == null)
            {
                return Ok(new { Code = "Error", Description = "Não foi possível confirmar o e-mail informado no site." });
            }

            if (novoUsuario.Succeeded)
            {
                return Ok(new { Code = "Success", Description = "E-mail confirmado com sucesso." });
            }

            return Ok(novoUsuario.Errors);
        }

        /// <summary>
        /// GerarEnvioConfirmacaoEmailAsync método para confirmar o email do usuário.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("gerarEmailConfirmacao/{email}")]
        public async Task<IActionResult> GerarEnvioConfirmacaoEmailAsync(string email)
        {
            var novoUsuario = await usuarioCommands.GerarEnvioConfirmacaoEmailAsync(email);

            if (novoUsuario == (int)HttpStatusCode.Accepted)
            {
                return Ok(new { Code = "Success", Description = "E-mail de confirmação enviado com sucesso." });
            }
            else
            {
                return Ok(new { Code = "Error", Description = "Não foi possível enviar e-mail informado no site." });
            }
        }

        /// <summary>
        /// GetAsync método obter se o email já foi confirmado.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("emailConfirmado/{email}")]
        public async Task<IActionResult> VerificarEmailConfirmadoAsync(string email) =>
            Ok(await usuarioCommands.VerificarEmailConfirmadoAsync(email));

        /// <summary>
        /// GetAsync método listar os perfis.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("perfis")]
        public async Task<IActionResult> PerfisAsync()
        {
            var perfis = await perfilQueries.ListarPerfisAsync();

            return Ok(perfis);
        }

        /// <summary>
        /// GetAsync método listar as permissões.
        /// </summary>
        /// <param name="perfil">perfil.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("permissoes/{perfil}")]
        public async Task<IActionResult> PermissoesAsync(string perfil)
        {
            var permissoes = await perfilQueries.ListarPermissoesAsync(perfil);

            return Ok(permissoes);
        }

        /// <summary>
        /// PostAsync método criar super usuário.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("seedSuperUsuario")]
        public async Task<IActionResult> SeedSuperUsuarioAsync()
        {
            var novoUsuario = await usuarioCommands.SeedSuperAdminAsync();

            if (novoUsuario)
            {
                return Ok("Super Usuário criado com sucesso");
            }

            return Ok(new
            {
                Code = "CreatedUserAdminSuccess",
                Description = "Usuário Admin criado com sucesso"
            });
        }

        /// <summary>
        /// PostAsync método criar novos perfis.
        /// </summary>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpPost("seedRoles")]
        public async Task<IActionResult> SeedRolesAsync()
        {
            await perfilCommands.SeedRolesAsync();

            return Ok(new
            {
                Code = "SeedRolesSuccess",
                Description = "Perfis criados com sucesso"
            });
        }

        /// <summary>
        /// GetAsync método obter a senha de um usuário.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.</returns>
        [HttpGet("senhaHash/email/{email}")]
        public async Task<IActionResult> GetSenhaHashAsync(string email) 
        {
            var senha = await loginQueries.GetPassawordHashAsync(email);

            if (senha.StartsWith("Erro"))
            {
                return Ok(new
                {
                    Mensagem = senha,
                    Senha = string.Empty,
                });
            }

            return Ok(new
            {
                Mensagem = string.Empty,
                Senha = senha,
            });
        }
    }
}