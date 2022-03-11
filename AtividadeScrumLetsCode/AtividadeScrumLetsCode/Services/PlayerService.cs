using AtividadeScrumLetsCode.Entities;
using AtividadeScrumLetsCode.Repositories;
using System;
using System.Threading;

namespace AtividadeScrumLetsCode.Services
{
    public class PlayerService
    {

        private PlayerRepository _playerRepository { get; set; }
        
        public PlayerService()
        {
            _playerRepository = new PlayerRepository();
        }

        public void CadastrarPlayer()
        {
            Console.Write("Nickname: ");
            var nick = Console.ReadLine();
            
            // Transferir a responsabilidade de validar para a entidade.
            while (!EhNickValido(nick))
            {
                Console.Write("Nickname: ");
                nick = Console.ReadLine();
            }

            var player = new Player(nick);

            _playerRepository.Create(player);

            MenuService.Iniciar();
        }

        private bool EhNickValido(string nickname)
        {
            if (string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("Nickname não pode ser vazio bobão...");
                Thread.Sleep(2000);
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
    }

    
}
