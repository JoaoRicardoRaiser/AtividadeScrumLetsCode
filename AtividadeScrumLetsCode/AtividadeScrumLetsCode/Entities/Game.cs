namespace AtividadeScrumLetsCode.Entities
{
    public class Game: EntidadeBase
    {
        public string NomeJogo { get; protected set; }
        public int MinimoJogadores { get; protected set; }
        public int MaximoJogadores { get; protected set; }

        protected Game(){}

        public Game(string nomeJogo, int minimoJogadores, int maximoJogadores)
        {
            NomeJogo = nomeJogo;
            MinimoJogadores = minimoJogadores;
            MaximoJogadores = maximoJogadores;
        }

        public void Update(string nomeJogo, int minimoJogadores, int maximoJogadores)
        {
            NomeJogo = nomeJogo;
            MinimoJogadores = minimoJogadores;
            MaximoJogadores = maximoJogadores;
        }
    }
}
