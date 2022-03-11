using AtividadeScrumLetsCode.Entities;
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
    }
}
