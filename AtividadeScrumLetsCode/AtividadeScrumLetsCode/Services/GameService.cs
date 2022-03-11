using AtividadeScrumLetsCode.Entities;
using AtividadeScrumLetsCode.Repositories;
using System;
using System.Threading;

namespace AtividadeScrumLetsCode.Services
{
    public class GameService
    {

        private GameRepository _gameRepository { get; set; }

        public GameService()
        {
            _gameRepository = new GameRepository();
        }
        
        public void CadastrarJogo()
        {
            Console.Write("Nome do Jogo: ");
            var nomeJogo = Console.ReadLine();

            while (!EhNomeValido(nomeJogo))
            {
                Console.Write("Nome do Jogo: ");
                nomeJogo = Console.ReadLine();
            }

            Console.Write("Quantidade Mínima de Jogadores: ");
            var quatidadeMinima = Console.ReadLine();
            while (!EhQuantidadeValida(quatidadeMinima))
            {
                Console.Write("Quantidade Mínima de Jogadores: ");
                quatidadeMinima = Console.ReadLine();
            }


            Console.Write("Quantidade Máxima de Jogadores: ");
            var quatidadeMaxima = Console.ReadLine();
            while (!EhQuantidadeValida(quatidadeMaxima))
            {
                Console.Write("Quantidade Máxima de Jogadores: ");
                quatidadeMaxima = Console.ReadLine();
            }

            var player = new Game(nomeJogo, Convert.ToInt32(quatidadeMinima), Convert.ToInt32(quatidadeMaxima));
            _gameRepository.Create(player);

            MenuService.Iniciar();
        }

        private bool EhNomeValido(string nomeJogo)
        {
            if (string.IsNullOrWhiteSpace(nomeJogo))
            {
                Console.WriteLine("o nome do jogo não pode ser vazio bobão...");
                Thread.Sleep(2000);
                return false;
            }

            var gameSalvo = _gameRepository.GetByNickname(nomeJogo);

            if (gameSalvo != null)
            {
                Console.WriteLine("Já existente jogo com esse nome bobão, coloca outro ai...");
                Thread.Sleep(2000);
                return false;
            }

            return true;
        }

        private static bool EhQuantidadeValida(string quantidade)
        {
            var podeConverter = int.TryParse(quantidade, out int quantidadeOut);

            if (quantidadeOut == 0 || !podeConverter)
            {
                Console.WriteLine("Quantidade inválida bobão, digite novamente...");
                Thread.Sleep(2000);
                return false;
            }

            return true;
        }
    }
}
