using AtividadeScrumLetsCode.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtividadeScrumLetsCode.Repositories
{
    public class PlayerRepository: GenericRepository<Player>
    {
        public PlayerRepository()
        {
            Host = Directory.GetCurrentDirectory() + @"..\..\..\..\Database\players.json";
        }
        
        public void Create(Player player)
        {
            var database = GetDatabase();
            database.Add(player);
            UpdateDatabase(database);
        }

        public Player GetByNickname(string nickname)
        {
            var database = GetDatabase();
            var playerSalvo = database.SingleOrDefault(x => x.Nickname.Equals(nickname));
            return playerSalvo;
        }

        public List<Player> GetByGame(Game game)
        {
            var database = GetDatabase();
            return database.Where(x => x.Jogos.Select(x => x.NomeJogo).Contains(game.NomeJogo)).ToList();
        }
    }
}
