using System;
using System.Collections.Generic;
using series_dotnet.Interfaces;

namespace series_dotnet
{
    public class SerieRepositorio : IRepository<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>();

        public List<Serie> Lista()
        {
            return listaSerie;
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }

        public void Insere(Serie objeto)
        {
            listaSerie.Add(objeto);
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Reinclui(int id)
        {
            listaSerie[id].Reincluir();
        }

        public void Atualiza(int id, Serie objeto)
        {
            listaSerie[id] = objeto;
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }
    }
}
