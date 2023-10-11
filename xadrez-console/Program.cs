using Exceptions;
using JogoXadrez;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;
using XadrezConsole;

PosicaoXadrez posicao = new PosicaoXadrez('a', 1);

Console.WriteLine(posicao);

Console.WriteLine(posicao.ToPosicao());

Console.ReadLine();