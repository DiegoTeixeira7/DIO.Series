using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarSeries();
						break;
					case "2":
						InserirSerie();
						break;
					case "3":
						AtualizarSerie();
						break;
					case "4":
						ExcluirSerie();
						break;
					case "5":
						VisualizarSerie();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						Console.WriteLine("Digite um argumento válido!");
                        break;
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
        }

        private static void ExcluirSerie()
		{	
			int indiceSerie = ObterIdSerie();

            if(indiceSerie == -1)
            {
                Console.WriteLine("Valor do id deve ser int");					
                return;
            }
            
            else if (indiceSerie >= repositorio.ProximoId())
            {
                Console.WriteLine("Valor do id não existe");					
                return;
            }

			repositorio.Exclui(indiceSerie);
		}

        private static void VisualizarSerie()
		{
			int indiceSerie = ObterIdSerie();

            if(indiceSerie == -1)
            {
                Console.WriteLine("Valor do id deve ser int");					
                return;
            }
            
            else if (indiceSerie >= repositorio.ProximoId())
            {
                Console.WriteLine("Valor do id não existe");					
                return;
            }

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
		}

        private static void AtualizarSerie()
		{
			int indiceSerie = ObterIdSerie();

            if(indiceSerie == -1)
            {
                Console.WriteLine("Valor do id deve ser int");					
                return;
            }
            
            else if (indiceSerie >= repositorio.ProximoId())
            {
                Console.WriteLine("Valor do id não existe");					
                return;
            }

			repositorio.Atualiza(indiceSerie, ObterSerie(indiceSerie));
		}
        private static void ListarSeries()
		{
			Console.WriteLine("Listar séries");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma série cadastrada.");
				return;
			}

			foreach (var serie in lista)
			{
                var excluido = serie.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirSerie()
		{
			Console.WriteLine("Inserir nova série");

			repositorio.Insere(ObterSerie(repositorio.ProximoId()));
		}
        
        private static int ObterIdSerie() {
            Console.Write("Digite o id da série: ");

            if(int.TryParse(Console.ReadLine(), out int indiceSerie))
                return indiceSerie;
            
            return -1;
        }

        private static Serie ObterSerie(int id) {
            foreach (int i in Enum.GetValues(typeof(Genero)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
			}

            Console.Write("Digite o gênero entre as opções acima: ");
            if(!int.TryParse(Console.ReadLine(), out int entradaGenero))
			    entradaGenero = 0;

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Série: ");
            if(!int.TryParse(Console.ReadLine(), out int entradaAno))
			    entradaAno = 2021;

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

			Serie novaSerie = new Serie(id: id,
										genero: (Genero)entradaGenero,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

            return novaSerie;
        }
        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Séries a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar séries");
			Console.WriteLine("2- Inserir nova série");
			Console.WriteLine("3- Atualizar série");
			Console.WriteLine("4- Excluir série");
			Console.WriteLine("5- Visualizar série");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}