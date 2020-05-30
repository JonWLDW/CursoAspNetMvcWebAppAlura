using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
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
			routerBuilder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastrarNovoLivro);
			routerBuilder.MapRoute("Livros/Detalhes/{id:int}", ExibirDetalhesDoLivro);

			var rotas = routerBuilder.Build();
			applicationBuilder.UseRouter(rotas);
		}

		private Task ExibirDetalhesDoLivro(HttpContext context)
		{
			int id = Convert.ToInt32(context.GetRouteValue("id"));
			var repo = new LivroRepositorioCSV();
			var livro = repo.Todos.First(l => l.Id.Equals(id));
			return context.Response.WriteAsync(livro.Detalhes());
		}

		public Task CadastrarNovoLivro(HttpContext context)
		{
			var livro = new Livro()
			{
				Titulo = context.GetRouteValue("nome").ToString(),
				Autor = context.GetRouteValue("autor").ToString()
			};

			var repo = new LivroRepositorioCSV();

			repo.Incluir(livro);
			return ImprimirLivrosParaLer(context);
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