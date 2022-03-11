namespace AtividadeScrumLetsCode.Entities
{
    public class Player: EntidadeBase
    {
        public string Nickname { get; protected set; }

        protected Player(){}

        public Player(string nickname)
        {
            Nickname = nickname;
        }

        public void Update(string nome, string email)
        {
            Nickname = nome;
        }
    }
}
