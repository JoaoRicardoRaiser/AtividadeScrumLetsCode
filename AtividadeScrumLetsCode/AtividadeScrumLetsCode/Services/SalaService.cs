using AtividadeScrumLetsCode.Entities;
using AtividadeScrumLetsCode.Repositories;
using AtividadeScrumLetsCode.Utils;
using System;
using System.Collections.Generic;
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

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Seja bem vindo à LetsCode Player\n");

            Console.WriteLine("Digite a opção que você deseja\n");
            Console.WriteLine("1 - Criar Sala");
            Console.WriteLine("2 - Buscar Sala");
            Console.WriteLine("0 - Voltar\n");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {

                case "1":
                    CriarSala();
                    break;

                case "2":
                    BuscarSala();
                    break;

                case "0":
                    MenuService.Iniciar();
                    break;

                default:
                    Console.WriteLine("\nOpção invalida, tente novamente...\n");
                    Thread.Sleep(2000);
                    Menu();
                    break;
            }
        }

        public void CriarSala()
        {
            try
            {
                Console.Clear();
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

                Console.WriteLine("Você deseja adicionar algum amigo na sala? (s/n)");
                var deveAdicionarAmigo = Console.ReadLine();

                if (deveAdicionarAmigo.Equals("s", StringComparison.InvariantCultureIgnoreCase))
                    AdicionarJogadoresNaSala(jogoEscolhido, sala);

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

        public void BuscarSala()
        {
            try
            {
                Console.Clear();
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

                var salaDisponivel = _salaRepository.GetByGame(jogoEscolhido).Where(x => x.Jogadores.Count < x.Jogo.MaximoJogadores).OrderByDescending(x => x.Jogadores.Count).FirstOrDefault();

                if (salaDisponivel == null)
                {
                    Console.WriteLine("Infelizmente não foi encontrada nenhuma sala aberta disponível para o jogo selecionado.");
                    Console.WriteLine("Crie sua própria sala ou aguarde até que uma seja aberta");
                    Thread.Sleep(4000);
                }
                else
                {
                    salaDisponivel.AddPlayer(playerSalvo);
                    _salaRepository.Update(salaDisponivel);
                    Console.WriteLine($"Você foi adicionado na sala com id '{salaDisponivel.Id}'");
                }

                Thread.Sleep(3000);
                MenuService.Iniciar();
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

        private void AdicionarJogadoresNaSala(Game game, Sala sala)
        {
            string adicionarNovoGame;
            var jogadoresSalvos = _playerRepository.GetByGame(game);
            var nomesDosJogadores = jogadoresSalvos.Select(x => x.Nickname);
            do
            {
                Console.Clear();
                Console.WriteLine("Jogadores Disponíveis: ");

                var jogadoresNaSala = jogadoresSalvos.Where(x => sala.Jogadores.Select(x => x.Nickname).Contains(x.Nickname)).ToList();
                jogadoresSalvos = jogadoresSalvos.Except(jogadoresNaSala).ToList();
                Utilidades.MostrarJogadores(jogadoresSalvos);

                Console.Write("\nDigite o nome do Jogador: ");
                var nickDoJogador = Console.ReadLine();

                if (nomesDosJogadores.Contains(nickDoJogador))
                {
                    var player = jogadoresSalvos.SingleOrDefault(x => x.Nickname == nickDoJogador);
                    sala.AddPlayer(player);
                    Console.WriteLine("Player adicionado com sucesso!");
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"Jogador com o nick '{nickDoJogador}' não existe... Olha a lista acima, bobão.\n");
                }

                Console.Write("Deseja inserir mais um jogo na sua lista de interesse? (s/n) bobão: ");
                adicionarNovoGame = Console.ReadLine();
                Console.Clear();
            }
            while (adicionarNovoGame.Equals("s", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
