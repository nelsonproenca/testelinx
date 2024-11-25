using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RVC.Intranet4.Web
{
    /// <summary>
    /// Classe Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Classe Program.
        /// </summary>
        /// <param name="args">Argumento para iniciar a aplicação.</param>
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Prints the given name..
        /// </summary>
        /// <param name="args">Argumento para iniciar a aplicação.</param>
        /// <returns>Create host builder.</returns>returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://localhost:443", "http://localhost:80");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
