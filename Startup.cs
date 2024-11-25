using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RVC.Intranet4.Autenticacao.Persistence;
using RVC.Intranet4.Core.Settings;

namespace RVC.Intranet4.Web
{
    /// <summary>
    /// Classe Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// Classe Startup.
        /// </summary>
        /// <param name="configuration">Argumento para iniciar a configuração da classe startup.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets initializes a new instance of the <see cref="Startup"/> class.
        /// Classe Startup.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets initializes a new instance of the <see cref="Startup"/> class.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="args">Argumento para Iniciar a criação do Host a aplicação.</param>
        /// <returns>Retorna a criação do Host.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(builder =>
          {
              builder.UseStartup<Startup>();
              builder.UseUrls("http://localhost:80/");
          });

        /// <summary>
        /// Gets initializes a new instance of the <see cref="Startup"/> class.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Argumento para iniciar a aplicação.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()));
            services.AddCors(c => c.AddPolicy("AllowMethod", options => options.AllowAnyMethod()));
            services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()));

            var sectionIdentityServerSettings = Configuration.GetSection("IdentityServerSettings");
            var sectionReceitaSettings = Configuration.GetSection("ReceitaSettings");
            var sectionNFSeSettings = Configuration.GetSection("NFSeSettings");
            var sectionOmieSettings = Configuration.GetSection("OmieSettings");
            var sectionOpenAISettings = Configuration.GetSection("OpenAISettings");
            var sectionIntegracaoBancariaSettings = Configuration.GetSection("IntegracaoBancariaSettings");
            var sectionNotificacaoSettings = Configuration.GetSection("NotificacaoSettings");
            var sectionIntranet40Settings = Configuration.GetSection("Intranet40Settings");

            IdentityServerSettings identityServerSettings = sectionIdentityServerSettings.Get<IdentityServerSettings>();
            ReceitaSettings receitaSettings = sectionReceitaSettings.Get<ReceitaSettings>();
            NFSeSettings nfSeSettings = sectionNFSeSettings.Get<NFSeSettings>();
            OmieSettings omieSettings = sectionOmieSettings.Get<OmieSettings>();
            OpenAISettings openAISettings = sectionOpenAISettings.Get<OpenAISettings>();
            IntegracaoBancariaSettings integracaoBancariaSettings = sectionOpenAISettings.Get<IntegracaoBancariaSettings>();
            NotificacaoSettings notificacaoSettings = sectionNotificacaoSettings.Get<NotificacaoSettings>();
            Intranet40Settings intranet40Settings = sectionIntranet40Settings.Get<Intranet40Settings>();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;

                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));

            services.AddHttpClient(
                "httpClientReceita",
                client =>
                {
                    client.BaseAddress = new Uri(receitaSettings.UrlBase);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", receitaSettings.Token);
                });

            services.AddHttpClient(
                "httpClientIntranet40",
                client =>
                {
                    client.BaseAddress = new Uri(intranet40Settings.UrlBase);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            var ambienteNSfe = Configuration.GetSection("AmbienteNFSe").Get<string>();
            var urlBase = ambienteNSfe == "Homologacao" ? nfSeSettings.UrlHomologacao : nfSeSettings.UrlProducao;
            services.AddHttpClient(
                "httpClientNSfe",
                client =>
                {
                    client.BaseAddress = new Uri(urlBase);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            services.AddHttpClient(
                "httpClientOmie",
                client =>
                {
                    client.BaseAddress = new Uri(omieSettings.UrlBase);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            // Dependency Injection Container
            Autenticacao.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Core.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Funcionarios.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Financeiros.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Pipelines.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            TimeSheets.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Avaliacoes.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Escritorios.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Clientes.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Menus.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Treinamentos.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Legados.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Shareds.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Programacoes.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);
            Aprovacoes.Dic.DependencyInjectionContainer.ConfigureServices(services, Configuration);

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                string description = Configuration.GetSection("ConnStringPrincipal").Get<string>() switch
                {
                    "Intranet4Dev" => "<b>Environment:</b> PROD<br />",
                    "Intranet4" => "<b>Environment:</b> DEV<br />",
                    _ => "<b>Environment:</b> HOMOLOG<br />",
                };

                // version
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API - Intranet 4.0",
                    Description = "<b>Descrição:</b> <i>API para interagir com o projeto de Frontend da Intranet e Power Apps da RVC.</i><br />" +
                      "<b>Publish Date:</b> 2024.04.12<br />" +
                      "<b>Version:</b> 4.0.16.68.97.240412<br />" +
                      "<b>TargetFramework:</b> netcoreapp3.1<br />" +
                      "<b>Identity Server 4</b><br />" +
                      "<b>Authorization:</b> JWT Authorization (Bearer)<br />" +
                      description
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n " +
                    "Enter 'Your token in the text input below.\r\n\r\n" +
                    "Example: \"eyJhbGciOiJIUzI1NiIsInR5...\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "RVC.Intranet4.Web.xml"));
            });

            InitializeApplicationServices(services);
        }

        /// <summary>
        /// Gets initializes a new instance of the <see cref="Startup"/> class.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Argumento para iniciar a aplicação.</param>
        /// <param name="env">Argumento para a interface Enviroment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "API - Intranet 4.0");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }

        private void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddAutoMapper(new[] 
            {
                typeof(Escritorios.Application.Area.Mappings.AreaMappings),
                typeof(Escritorios.Application.Cargo.Mappings.CargoMappings),
                typeof(Escritorios.Application.Escritorio.Mappings.EscritorioMappings),
                typeof(Escritorios.Application.Empresa.Mappings.EmpresaMappings),
                typeof(Escritorios.Application.ValorCargo.Mappings.ValorCargoMappings),
                typeof(Escritorios.Application.ScaleRate.Mappings.ScaleRateMappings),
                typeof(Escritorios.Application.Funcao.Mappings.FuncaoMappings),
                typeof(Escritorios.Application.WipProcessamento.Mappings.WipProcessamentoMappings),
                typeof(Clientes.Application.Cliente.Mappings.ClienteMappings),
                typeof(Clientes.Application.Contato.Mappings.ContatoMappings),
                typeof(Clientes.Application.Municipio.Mappings.MunicipioMappings),
                typeof(Clientes.Application.RamoAtividade.Mappings.RamoAtividadeMappings),
                typeof(Avaliacoes.Application.ParametroAvaliacao.Mappings.ParametroAvaliacaoMappings),
                typeof(Avaliacoes.Application.Avaliacao.Mappings.AvaliacaoMappings),
                typeof(Avaliacoes.Application.AvaliacaoConsolidada.Mappings.AvaliacaoConsolidadaMappings),
                typeof(Menus.Application.Menu.Mappings.MenuMappings),
                typeof(Menus.Application.Notificacao.Mappings.NotificacaoMappings),
                typeof(Funcionarios.Application.Arquivo.Mappings.ArquivoMappings),
                typeof(Funcionarios.Application.Contato.Mappings.ContatoMappings),
                typeof(Funcionarios.Application.Dependente.Mappings.DependenteMappings),
                typeof(Funcionarios.Application.Detalhe.Mappings.DetalheMappings),
                typeof(Funcionarios.Application.Documento.Mappings.DocumentoMappings),
                typeof(Funcionarios.Application.Endereco.Mappings.EnderecoMappings),
                typeof(Funcionarios.Application.ExperienciaProfissional.Mappings.ExperienciaProfissionalMappings),
                typeof(Funcionarios.Application.Formacao.Mappings.FormacaoMappings),
                typeof(Funcionarios.Application.Funcionario.Mappings.FuncionarioMappings),
                typeof(Funcionarios.Application.Idioma.Mappings.IdiomaMappings),
                typeof(Funcionarios.Application.InformacaoBancaria.Mappings.InformacaoBancariaMappings),
                typeof(Funcionarios.Application.SolicitacaoBeneficio.Mappings.SolicitacaoBeneficioMappings),
                typeof(Funcionarios.Application.TransferenciaProfissional.Mappings.TransferenciaProfissionalMappings),
                typeof(Pipelines.Application.ControleProposta.Mappings.ControlePropostaMappings),
                typeof(Pipelines.Application.FerramentaTI.Mappings.FerramentaTIMappings),
                typeof(Pipelines.Application.Forecast.Mappings.ForecastMappings),
                typeof(Pipelines.Application.Orcamento.Mappings.OrcamentoMappings),
                typeof(Pipelines.Application.OrcamentoDetalhe.Mappings.OrcamentoDetalheMappings),
                typeof(Pipelines.Application.OrcamentoHistorico.Mappings.OrcamentoHistoricoMappings),
                typeof(Pipelines.Application.Parceiro.Mappings.ParceiroMappings),
                typeof(Pipelines.Application.Participacao.Mappings.ParticipacaoMappings),
                typeof(Pipelines.Application.Pipeline.Mappings.PipelineMappings),
                typeof(Pipelines.Application.Timeline.Mappings.TimelineMappings),
                typeof(Pipelines.Application.FollowUp.Mappings.FollowUpMappings),
                typeof(Pipelines.Application.NaturezaTrabalho.Mappings.NaturezaTrabalhoMappings),
                typeof(Pipelines.Application.TributoPadrao.Mappings.TributoPadraoMappings),
                typeof(Pipelines.Application.TeseJuridicoTributario.Mappings.TeseJuridicoTributarioMappings),
                typeof(Pipelines.Application.Status.Mappings.StatusMappings),
                typeof(Financeiros.Application.Faturamento.Mappings.FaturamentoMappings),
                typeof(Financeiros.Application.ImpostoEmpresa.Mappings.ImpostoEmpresaMappings),
                typeof(Financeiros.Application.Parcela.Mappings.ParcelaMappings),
                typeof(Financeiros.Application.Recebimento.Mappings.RecebimentoMappings),
                typeof(Financeiros.Application.SolicitacaoCompra.Mappings.SolicitacaoCompraMappings),
                typeof(Financeiros.Application.Comprovante.Mappings.ComprovanteMappings),
                typeof(Financeiros.Application.Despesa.Mappings.DespesaMappings),
                typeof(Financeiros.Application.NotaDebito.Mappings.NotaDebitoMappings),
                typeof(Financeiros.Application.Pagamento.Mappings.PagamentoMappings),
                typeof(Financeiros.Application.PrestacaoConta.Mappings.PrestacaoContaMappings),
                typeof(Financeiros.Application.CategoriaOMIE.Mappings.CategoriaOMIEMappings),
                typeof(Financeiros.Application.PlanoConta.Mappings.PlanoContaMappings),
                typeof(Financeiros.Application.Budget.Mappings.BudgetMappings),
                typeof(Financeiros.Application.BIScaleRvc.Mappings.BIScaleRvcMappings),
                typeof(Financeiros.Application.BIScaleArea.Mappings.BIScaleAreaMappings),
                typeof(Financeiros.Application.ProgramacaoPagamento.Mappings.ProgramacaoPagamentoMappings),
                typeof(TimeSheets.Application.ApontamentoHora.Mappings.ApontamentoHoraMappings),
                typeof(TimeSheets.Application.Aprovacao.Mappings.AprovacaoMappings),
                typeof(TimeSheets.Application.AtestadoLicenca.Mappings.AtestadoLicencaMappings),
                typeof(TimeSheets.Application.Feriado.Mappings.FeriadoMappings),
                typeof(TimeSheets.Application.SolicitacaoFerias.Mappings.SolicitacaoFeriasMappings),
                typeof(TimeSheets.Application.ParametroTimesheet.Mappings.ParametroTimesheetMappings),
                typeof(TimeSheets.Application.ReaberturaTimesheet.Mappings.ReaberturaTimesheetMappings),
                typeof(TimeSheets.Application.SolicitacaoHoraExtra.Mappings.SolicitacaoHoraExtraMappings),
                typeof(TimeSheets.Application.SolicitacaoDescanso.Mappings.SolicitacaoDescansoMappings),
                typeof(TimeSheets.Application.ApontamentoHoraConclusao.Mappings.ApontamentoHoraConclusaoMappings),
                typeof(Treinamentos.Application.Arquivo.Mappings.ArquivoMappings),
                typeof(Legados.Application.Legado.Mappings.LegadoMappings),
                typeof(Legados.Application.ProjetoAdministrativo.Mappings.ProjetoAdministrativoMappings),
                typeof(Programacoes.Application.Programacao.Mappings.ProgramacaoMappings),
                typeof(Aprovacoes.Application.Aprovacao.Mappings.AprovacaoMappings),
                typeof(Core.Infrastructure.IntegracaoBancaria.Cnab240.Mappings.Cnab240Mappings),
                typeof(Avaliacoes.Services.Mappings.ServicesMappings),
                typeof(Financeiros.Service.Mappings.ServicesMappings),
                typeof(Pipelines.Service.Mappings.ServicesMappings),
                typeof(TimeSheets.Service.Mappings.ServicesMappings),
                typeof(Programacoes.Service.Mappings.ServicesMappings),
                typeof(Aprovacoes.Service.Mappings.ServicesMappings),
            });
        }
    }
}