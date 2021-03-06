using System;
using System.Threading;

namespace AtividadeScrumLetsCode.Services
{
    class MenuService
    {
        public static void Iniciar()
        {
            var playerService = new PlayerService();
            var gameService = new GameService();
            var salaService = new SalaService();

            Console.Title = "Lets Code Player";
            Console.Clear();
            Console.WriteLine("Seja bem vindo à LetsCode Player\n");

            Console.WriteLine("Digite a opção que você deseja\n");
            Console.WriteLine("1 - Cadastrar Player");
            Console.WriteLine("2 - Cadastrar Jogo");
            Console.WriteLine("3 - Iniciar Partida");
            Console.WriteLine("0 - Sair\n");
            Console.Write("Opção: ");

            switch (Console.ReadLine())
            {
                case "1":
                    playerService.CadastrarPlayer();
                    break;

                case "2":
                    gameService.CadastrarJogo();
                    break;

                case "3":
                    salaService.Menu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("\nOpção invalida, tente novamente...\n");
                    Thread.Sleep(2000);
                    Iniciar();
                    break;
            }

            
        }
    }
}
