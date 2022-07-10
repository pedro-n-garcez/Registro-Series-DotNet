using System;
using System.IO;

namespace series_dotnet
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            ConsoleKeyInfo opcaoUsuario = ObterOpcaoUsuario();
            while (opcaoUsuario.Key != ConsoleKey.X)
            {
                switch (opcaoUsuario.Key)
				{
					case ConsoleKey.D1:
						Console.Clear();
						ListarSeries();
						break;
					case ConsoleKey.D2:
						Console.Clear();
						InserirSerie();
						Console.Clear();
						break;
					case ConsoleKey.D3:
						Console.Clear();
						AtualizarSerie();
						Console.Clear();
						break;
					case ConsoleKey.D4:
						Console.Clear();
						ExcluirSerie();
						Console.Clear();
						break;
					case ConsoleKey.D5:
						Console.Clear();
						ReincluirSerie();
						Console.Clear();
						break;
					case ConsoleKey.D6:
						Console.Clear();
						VisualizarSerie();
						break;
					case ConsoleKey.D7:
						Console.Clear();
						SalvarLista();
						break;
					case ConsoleKey.D8:
						Console.Clear();
						AbrirLista();
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				opcaoUsuario = ObterOpcaoUsuario();
            }
            Console.WriteLine("Obrigado por utilizar nossos serviços!");
            Console.WriteLine("Pressione qualquer tecla para sair.");
			Console.ReadKey();
        }

		private static void SalvarLista()
		{
			Console.Write("Forneça um nome para a sua lista (sem a extensão .csv): ");
			string nomeArquivo = Console.ReadLine() + ".csv";
			if (File.Exists(nomeArquivo))
			{
				Console.WriteLine("Já existe um lista com esse nome. Você deseja sobreescrevê-la? Sim ou não (Y/N)");
				if (ConfirmarEscolha())
				{
					Csv csv = new Csv();
					csv.ExportarArquivo(repositorio,nomeArquivo);
				}
			}
			else
			{
				Csv csv = new Csv();
				csv.ExportarArquivo(repositorio,nomeArquivo);
			}
		}

		private static void AbrirLista()
		{
			Console.Write("Informe o nome da lista (sem a extensão .csv): ");
			string nomeArquivo = Console.ReadLine() + ".csv";
			Console.WriteLine($"Você tem certeza que deseja abrir essa lista? Sua lista atual será apagada. Sim ou não (Y/N)");
            if (ConfirmarEscolha())
			{
				Csv csv = new Csv();
				csv.ImportarArquivo(nomeArquivo, repositorio);
			}
		}

        private static void ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Int32.Parse(Console.ReadLine());

            Console.WriteLine($"Você tem certeza que deseja excluir a série de #ID {indiceSerie}? Sim ou não (Y/N)");
            if (ConfirmarEscolha()){repositorio.Exclui(indiceSerie);}
		}

		private static void ReincluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Int32.Parse(Console.ReadLine());

			Console.WriteLine($"Você tem certeza que deseja reincluir a série de #ID {indiceSerie}? Sim ou não (Y/N)");
			if (ConfirmarEscolha()){repositorio.Reinclui(indiceSerie);}
		}

        private static void VisualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Int32.Parse(Console.ReadLine());

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        //seguir o conceito DRY e evitar repetição de código nas funções
        //AtualizarSerie() e InserirSerie(), criando a função abaixo que
        //pode ser utilizada pelas duas
        private static Serie GerarSerieComParametros(int id)
        {
			foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
			}

			Console.Write("Digite o gênero dentre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o título da série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o ano de início da série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a descrição da série: ");
			string entradaDescricao = Console.ReadLine();

			Serie serie = new Serie(id: id,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);
            return serie;
        }

        private static void AtualizarSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

            Console.WriteLine($"Você tem certeza que deseja atualizar a série de #ID {indiceSerie}? Sim ou não (Y/N)");
            if (ConfirmarEscolha()){repositorio.Atualiza(indiceSerie, GerarSerieComParametros(indiceSerie));}
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries:");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada!");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
				Console.WriteLine($"#ID {serie.retornaId()}: - {serie.retornaTitulo()} {(excluido ? "(Excluída)" : "")}");
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série:");
			repositorio.Insere(GerarSerieComParametros(repositorio.ProximoId()));
		}

        private static ConsoleKeyInfo ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("Bem vindo à sua Lista de Séries!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1 - Listar séries");
			Console.WriteLine("2 - Inserir nova série");
			Console.WriteLine("3 - Atualizar série");
			Console.WriteLine("4 - Excluir série");
			Console.WriteLine("5 - Reincluir série");
			Console.WriteLine("6 - Visualizar série");
			Console.WriteLine("7 - Salvar lista");
			Console.WriteLine("8 - Abrir lista existente");
			Console.WriteLine("X - Sair");
			Console.WriteLine();

			ConsoleKeyInfo opcaoUsuario = Console.ReadKey();
			Console.WriteLine();
			return opcaoUsuario;
		}

        private static bool ConfirmarEscolha()
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Y){return true;}
            else if (cki.Key == ConsoleKey.N){return false;}
            else {throw new ArgumentOutOfRangeException();}
        }
    }
}
