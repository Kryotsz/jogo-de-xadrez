﻿using Exceptions;
using JogoXadrez;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;
using XadrezConsole;

try
{
    PartidaXadrez partida = new PartidaXadrez();

    while (!partida.Terminada)
    {
        try
        {
            Console.Clear();
            Tela.ImprimirPartida(partida);

            Console.WriteLine();
            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaoDeOrigem(origem);

            bool[,] posicoesPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();

            Console.Clear();
            Tela.ImprimirTabuleiro(partida.Tabuleiro, posicoesPossiveis);

            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaoDeDestino(origem, destino);

            partida.RealizaJogada(origem, destino);
        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
    Console.Clear();
    Tela.ImprimirPartida(partida);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();