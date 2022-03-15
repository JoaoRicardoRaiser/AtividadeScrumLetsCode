using AtividadeScrumLetsCode.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtividadeScrumLetsCode.Repositories
{
    public class SalaRepository: GenericRepository<Sala>
    {

        public SalaRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Database\salas.json";
        }

        public void Create(Sala sala)
        {
            var database = GetDatabase();
            database.Add(sala);
            UpdateDatabase(database);
        }

        public List<Sala> GetByGame(Game game)
        {
            var database = GetDatabase();

            return database.Where(x => x.Jogo.NomeJogo == game.NomeJogo).ToList();
        }

        public void Update(Sala sala)
        {
            var database = GetDatabase();
            var salaSalva = database.SingleOrDefault(x => x.Id == sala.Id);
            database.Remove(salaSalva);
            database.Add(sala);
            UpdateDatabase(database);
        }
    }
}
