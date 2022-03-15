using System.Collections.Generic;

namespace AtividadeScrumLetsCode.Entities
{
    public class Sala: EntidadeBase
    {
        public Game Jogo { get; protected set; }
        public List<Player> Jogadores { get; protected set; }

        protected Sala() {}

        public Sala(Game jogo, List<Player> jogadores)
        {
            Jogo = jogo;
            Jogadores = jogadores;
        }

        public void AddPlayer(Player player)
        {
            Jogadores.Add(player);
        }
    }
}
