using System;
using System.IO;
using System.Text.RegularExpressions;

namespace series_dotnet
{
    public class Csv
    {

        public Csv(){}

        public void ImportarArquivo(string nome, SerieRepositorio rep)
        {
            Serie serie;

            try
            {
                var linhas = File.ReadLines(nome);
                Regex regex = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                foreach (var linha in linhas)
                {
                    string[] listaLinhas = regex.Split(linha);
                    //remover aspas das linhas de indice 2 e 3
                    for (int i = 2; i <= 3; i++)
                    {
                        listaLinhas[i] = listaLinhas[i].TrimStart('"');
                        listaLinhas[i] = listaLinhas[i].TrimEnd('"');
                    }

                    serie = new Serie(
                        Int32.Parse(listaLinhas[0]),
                        (Genero)Int32.Parse(listaLinhas[1]),
                        listaLinhas[2],
                        listaLinhas[3],
                        Int32.Parse(listaLinhas[4]));
                    if (listaLinhas[5]=="true")
                    {
                        serie.Excluir();
                    }
                    rep.Insere(serie);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Arquivo CSV não encontrado.");
                Console.WriteLine(e.Message);
            }
        }

        public void ExportarArquivo(SerieRepositorio repo, string nomeArquivo)
        {
            var lista = repo.Lista();
            using (StreamWriter sw = new StreamWriter(nomeArquivo,false))
            {
                foreach (var serie in lista)
                {
                    sw.Write(serie.retornaId() + ",");
                    sw.Write(serie.retornaGenero() + ",");
                    sw.Write($@"""{serie.retornaTitulo()}""" + ",");
                    sw.Write($@"""{serie.retornaDescricao()}""" + ",");
                    sw.Write(serie.retornaAno() + ",");
                    sw.Write(serie.retornaExcluido().ToString().ToLower() + "\n");
                }
            }
        }
    }
}
