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
			var caminhos = new Dictionary<string, string>
			{
				{"/Livros/ParaLer", repo.ParaLer.ToString() },
				{"/Livros/Lendo", repo.Lendo.ToString() },
				{"/Livros/Lidos", repo.Lidos.ToString()}
			};

			var path = context.Request.Path;
			if (caminhos.ContainsKey(path))
				return context.Response.WriteAsync(caminhos[path]);

			return context.Response.WriteAsync("Caminho inexistente.");
		}

		public Task ImprimirLivrosParaLer(HttpContext context)
		{
			var repo = new LivroRepositorioCSV();
			return context.Response.WriteAsync(repo.ParaLer.ToString());
		}
	}
}