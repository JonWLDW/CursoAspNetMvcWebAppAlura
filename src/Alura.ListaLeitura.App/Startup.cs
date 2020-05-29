using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRouting();
		}

		public void Configure(IApplicationBuilder applicationBuilder)
		{
			var routerBuilder = new RouteBuilder(applicationBuilder);
			routerBuilder.MapRoute("Livros/ParaLer", ImprimirLivrosParaLer);
			routerBuilder.MapRoute("Livros/Lendo", ImprimirLivrosLendo);
			routerBuilder.MapRoute("Livros/Lidos", ImprimirLivrosLidos);

			var rotas = routerBuilder.Build();
			applicationBuilder.UseRouter(rotas);
		}

		public Task FazerRoteamento(HttpContext context)
		{
			context.Response.StatusCode = StatusCodes.Status404NotFound;
			return context.Response.WriteAsync("Caminho inexistente.");
		}

		public Task ImprimirLivrosParaLer(HttpContext context)
		{
			var repo = new LivroRepositorioCSV();
			return context.Response.WriteAsync(repo.ParaLer.ToString());
		}

		public Task ImprimirLivrosLendo(HttpContext context)
		{
			var repo = new LivroRepositorioCSV();
			return context.Response.WriteAsync(repo.Lendo.ToString());
		}

		public Task ImprimirLivrosLidos(HttpContext context)
		{
			var repo = new LivroRepositorioCSV();
			return context.Response.WriteAsync(repo.Lidos.ToString());
		}
	}
}