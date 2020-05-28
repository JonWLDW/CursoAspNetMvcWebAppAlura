using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
	public class Startup
	{
		public void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.Run(FazerRoteamento);
		}

		public Task FazerRoteamento(HttpContext context)
		{
			var repo = new LivroRepositorioCSV();
			var caminhos = new Dictionary<string, RequestDelegate>
			{
				{"/Livros/ParaLer", ImprimirLivrosParaLer },
				{"/Livros/Lendo", ImprimirLivrosLendo },
				{"/Livros/Lidos", ImprimirLivrosLidos }
			};

			var path = context.Request.Path;
			if (caminhos.ContainsKey(path))
				return caminhos[path].Invoke(context);

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