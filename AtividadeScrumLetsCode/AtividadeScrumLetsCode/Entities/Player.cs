using System.Collections.Generic;

namespace AtividadeScrumLetsCode.Entities
{
    public class Player: EntidadeBase
    {
        public string Nickname { get; protected set; }
        public List<Game> Jogos { get; protected set; } = new();

        protected Player(){}

        public Player(string nickname)
        {
            Nickname = nickname;
        }

        public void Update(string nome, string email)
        {
            Nickname = nome;
        }

        public void AddGame(Game game)
        {
            Jogos.Add(game);
        }
    }
}
