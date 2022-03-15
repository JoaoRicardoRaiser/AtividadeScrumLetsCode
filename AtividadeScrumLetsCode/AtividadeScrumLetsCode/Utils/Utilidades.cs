using AtividadeScrumLetsCode.Entities;
using System;
using System.Collections.Generic;

namespace AtividadeScrumLetsCode.Utils
{
    public static class Utilidades
    {
        public static void MostrarJogos(List<Game> jogosSalvos)
        {
            foreach (var jogo in jogosSalvos)
            {
                Console.WriteLine($"- {jogo}");
            }
        }
    }
}
