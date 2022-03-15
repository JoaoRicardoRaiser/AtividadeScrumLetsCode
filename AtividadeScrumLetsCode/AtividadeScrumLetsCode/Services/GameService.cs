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
            try
            {
                Console.Clear();
                Console.WriteLine("Cadastro de Game\n");

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

                var game = new Game(nomeJogo, Convert.ToInt32(quatidadeMinima), Convert.ToInt32(quatidadeMaxima));
                _gameRepository.Create(game);

                Console.Clear();
                Console.WriteLine($"Game com nome: '{game.NomeJogo}' criado com sucesso!");
                Thread.Sleep(2000);
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine($"\nOcorreu um erro ao cadastrar game...\n{e}\n");
                Console.WriteLine($"Nenhuma mudança será realizada na base de dados.\n");
                Console.ResetColor();
                Console.WriteLine($"Aperte qualquer botão para continuar...");
                Console.Read();
            }
            
            MenuService.Iniciar();
        }

        public bool ExistemJogos()
        {
            var jogosSalvos = _gameRepository.GetAll();
            if (jogosSalvos.Count == 0)
            {
                Console.WriteLine("\nAntes de cadastrar um Player cadastre um Jogo!\n");
                Thread.Sleep(3000);

                return false;
            }
            return true;
        }

        private bool EhNomeValido(string nomeJogo)
        {
            if (string.IsNullOrWhiteSpace(nomeJogo))
            {
                Console.WriteLine("\nO nome do jogo não pode ser vazio bobão...");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }

            var gameSalvo = _gameRepository.GetByNickname(nomeJogo);

            if (gameSalvo != null)
            {
                Console.WriteLine("\nJá existente jogo com esse nome bobão, coloca outro ai...");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }

            return true;
        }

        private static bool EhQuantidadeValida(string quantidade)
        {
            var podeConverter = int.TryParse(quantidade, out int quantidadeOut);

            if (quantidadeOut == 0 || !podeConverter)
            {
                Console.WriteLine("\nQuantidade inválida bobão, digite novamente...");
                Thread.Sleep(2000);
                Console.Clear();
                return false;
            }

            return true;
        }
    }
}
