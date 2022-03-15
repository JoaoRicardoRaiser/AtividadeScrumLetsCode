using AtividadeScrumLetsCode.Entities;
using AtividadeScrumLetsCode.Repositories;
using AtividadeScrumLetsCode.Utils;
using System;
using System.Linq;
using System.Threading;

namespace AtividadeScrumLetsCode.Services
{
    public class SalaService
    {
        private readonly PlayerRepository _playerRepository;
        private readonly SalaRepository _salaRepository;

        public SalaService()
        {
            _playerRepository = new PlayerRepository();
            _salaRepository = new SalaRepository();
        }

        public void CriarSala()
        {
            try
            {
                Console.WriteLine("Gerenciador de Partida\n");

                Console.Write("Nickname: ");

                var nickname = Console.ReadLine();
                var playerSalvo = _playerRepository.GetByNickname(nickname);

                while (playerSalvo == null)
                {
                    Console.WriteLine($"Não foi possível encontrar nickname '{nickname}', verifique e digite novamente");
                    Thread.Sleep(2000);
                    Console.Clear();

                    Console.Write("Nickname: ");
                    nickname = Console.ReadLine();
                    playerSalvo = _playerRepository.GetByNickname(nickname);
                }

                Console.WriteLine();
                Console.WriteLine("Escolha o game: ");
                Utilidades.MostrarJogos(playerSalvo.Jogos);
                Console.Write("\nDigite o nome do jogo: ");

                var respostaJogoEscolhido = Console.ReadLine();
                var jogoEscolhido = playerSalvo.Jogos.SingleOrDefault(x => respostaJogoEscolhido.Equals(x.NomeJogo, StringComparison.InvariantCultureIgnoreCase));

                while (jogoEscolhido == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Não foi possível encontrar jogo com nome '{respostaJogoEscolhido}', verifique e digite novamente");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    Console.Clear();

                    Console.WriteLine("Escolha o game: ");
                    Utilidades.MostrarJogos(playerSalvo.Jogos);
                    Console.Write("\nDigite o nome do jogo: ");
                    respostaJogoEscolhido = Console.ReadLine();
                    jogoEscolhido = playerSalvo.Jogos.SingleOrDefault(x => respostaJogoEscolhido.Equals(x.NomeJogo, StringComparison.InvariantCultureIgnoreCase));
                }

                var sala = new Sala(jogoEscolhido, new() { playerSalvo });

                _salaRepository.Create(sala);

                Console.WriteLine($"Sala com Id: '{sala.Id}' criada com sucesso!");
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.Clear();
                Console.WriteLine($"\nOcorreu um erro ao cadastrar sala...\n{e}\n");
                Console.WriteLine($"Nenhuma mudança será realizada na base de dados.\n");
                Console.ResetColor();

                Console.WriteLine($"Aperte qualquer botão para continuar...");
                Console.Read();
            }

            MenuService.Iniciar();
        }
    }
}
