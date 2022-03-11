using AtividadeScrumLetsCode.Entities;
using AtividadeScrumLetsCode.Repositories;
using System;
using System.Linq;
using System.Threading;

namespace AtividadeScrumLetsCode.Services
{
    public class PlayerService
    {

        private PlayerRepository _playerRepository { get; set; }
        private GameRepository _gameRepository { get; set; }
        
        public PlayerService()
        {
            _playerRepository = new PlayerRepository();
            _gameRepository = new GameRepository();
        }

        public void CadastrarPlayer()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Cadastro de Player\n");
                Console.Write("Nickname: ");
                var nick = Console.ReadLine();

                while (!EhNickValido(nick))
                {
                    Console.Write("Nickname: ");
                    nick = Console.ReadLine();
                }

                var player = new Player(nick);

                Console.Write("Quer adicionar algum jogo de interesse? (s/n) bobão: ");
                var deveAdicionarGame = Console.ReadLine();

                if (deveAdicionarGame.Equals("s", StringComparison.InvariantCultureIgnoreCase))
                    AdicionarGames(player);

                _playerRepository.Create(player);
                Console.Clear();
                Console.WriteLine($"Player com nome: '{player.Nickname}' criado com sucesso!");
                Thread.Sleep(2000);
                
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.Clear();
                Console.WriteLine($"\nOcorreu um erro ao cadastrar player...\n{e}\n");
                Console.WriteLine($"Nenhuma mudança será realizada na base de dados.\n");
                Console.ResetColor();
                
                Console.WriteLine($"Aperte qualquer botão para continuar...");
                Console.Read();
                
            }
            
            MenuService.Iniciar();
        }

        private bool EhNickValido(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Nickname não pode ser vazio bobão...");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }

            var nicknameSalvo = _playerRepository.GetByNickname(nickname);

            if(nicknameSalvo != null)
            {
                Console.WriteLine("Já existente jogador com esse nickname bobão, coloca outro ai...");
                Thread.Sleep(2000);
                return false;
            }

            return true;
        }

        private void AdicionarGames(Player player)
        {

            string adicionarNovoGame;
            var jogosSalvos = _gameRepository.GetAll();

            do
            {
                Console.Clear();
                Console.WriteLine("Jogos Disponíveis: ");
                foreach (var jogo in jogosSalvos)
                {
                    Console.WriteLine($"- {jogo}");
                }
                Console.Write("\nDigite o nome do Jogo: ");
                var nomeDoJogo = Console.ReadLine();
                var jogoSalvo = jogosSalvos.SingleOrDefault(x => nomeDoJogo.Equals(x.NomeJogo, StringComparison.InvariantCultureIgnoreCase));

                if (jogoSalvo != null)
                {
                    player.AddGame(jogoSalvo);
                    Console.WriteLine("Jogo Adicionado com sucesso!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Jogo não existe... Olha a lista acima, bobão.\n");
                }

                Console.Write("Deseja inserir mais um jogo na sua lista de interesse? (s/n) bobão: ");
                adicionarNovoGame = Console.ReadLine();
                Console.Clear();
            }
            while (adicionarNovoGame.Equals("s", StringComparison.InvariantCultureIgnoreCase));
        }
    }

}
