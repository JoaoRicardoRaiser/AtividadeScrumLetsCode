using AtividadeScrumLetsCode.Entities;
using System;
using System.IO;
using System.Linq;

namespace AtividadeScrumLetsCode.Repositories
{
    public class GameRepository: GenericRepository<Game>
    {
        public GameRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Database\games.json";
        }

        public void Create(Game player)
        {
            var database = GetDatabase();
            database.Add(player);
            UpdateDatabase(database);
        }

        public Game GetByNickname(string nomeJogo)
        {
            var database = GetDatabase();
            var gameSalvo = database.SingleOrDefault(x => x.NomeJogo.Equals(nomeJogo, StringComparison.InvariantCultureIgnoreCase));
            return gameSalvo;
        }
    }
}
