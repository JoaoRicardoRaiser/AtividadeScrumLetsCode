using AtividadeScrumLetsCode.Entities;
using System;
using System.Collections.Generic;

namespace AtividadeScrumLetsCode.Utils
{
    public static class Utilidades
    {
        public static void MostrarJogos(List<Game> jogos)
        {
            foreach (var jogo in jogos)
            {
                Console.WriteLine($"- {jogo}");
            }
        }

        public static void MostrarJogadores(List<Player> jogadores)
        {
            foreach (var jogador in jogadores)
            {
                Console.WriteLine($"- {jogador.Nickname}");
            }
        }
    }
}
