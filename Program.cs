using System;

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
						ListarSeries();
						break;
					case ConsoleKey.D2:
						InserirSerie();
						break;
					case ConsoleKey.D3:
						AtualizarSerie();
						break;
					case ConsoleKey.D4:
						ExcluirSerie();
						break;
					case ConsoleKey.D5:
						VisualizarSerie();
						break;
					case ConsoleKey.C:
						Console.Clear();
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
        private static void ExcluirSerie()
		{
			Console.Write("Digite o id da série: ");
			int indiceSerie = Int32.Parse(Console.ReadLine());

            Console.WriteLine($"Você tem certeza que deseja excluir a série de #ID {indiceSerie}? Sim ou não (Y/N)");
            if (ConfirmarEscolha()){repositorio.Exclui(indiceSerie);}
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
			Console.WriteLine("5 - Visualizar série");
			Console.WriteLine("C - Limpar Tela");
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
