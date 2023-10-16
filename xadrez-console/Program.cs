﻿using Exceptions;
using JogoXadrez;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;
using XadrezConsole;

try
{
    Tabuleiro tabuleiro = new Tabuleiro(8, 8);

    tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(0, 0));
    tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Preta), new Posicao(1, 3));
    tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Preta), new Posicao(0, 2));

    tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branca), new Posicao(3, 5));

    Tela.ImprimirTabuleiro(tabuleiro);
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();