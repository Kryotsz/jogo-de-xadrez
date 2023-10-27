using JogoXadrez;
using System.Data.Common;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
        }

        // método que imprime na tela o conjunto de peças capturadas de ambas as cores
        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        // método que imprime na tela os conjuntos de peças
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }

        // método pra imprimir o tabuleiro na tela, recebe o tabuleiro como parâmetro
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            // loop para imprimir o tabuleiro na tela
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                // imprime o número das linhas na lateral do tabuleiro
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    // chama o método pra imprimir a peça, passa a peça no tabuleiro como parâmetro
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                // quebra a linha
                Console.WriteLine();
            }
            // imprime a letra referente às colunas na parte debaixo do tabuleiro
            Console.WriteLine("  a b c d e f g h");
        }

        // método pra imprimir o tabuleiro na tela, recebe o tabuleiro como parâmetro
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            // variáveis para controlar a cor do fundo do console
            ConsoleColor fundoPadrao = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            // loop para imprimir o tabuleiro na tela
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                // imprime o número das linhas na lateral do tabuleiro
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    // se for uma posição possível
                    if (posicoesPossiveis[i, j])
                    {
                        // a cor do fundo ficará cinza
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        // a cor do fundo será a cor padrão do console
                        Console.BackgroundColor = fundoPadrao;
                    }
                    // chama o método pra imprimir a peça, passa a peça no tabuleiro como parâmetro
                    ImprimirPeca(tabuleiro.Peca(i, j));
                    // volta a cor do fundo ao padrão do console
                    Console.BackgroundColor = fundoPadrao;
                }
                // quebra a linha
                Console.WriteLine();
            }
            // imprime a letra referente às colunas na parte debaixo do tabuleiro
            Console.WriteLine("  a b c d e f g h");
            // volta a cor do fundo ao padrão do console
            Console.BackgroundColor = fundoPadrao;
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        // método que imprime a peça (recebe a peça por parâmetro)
        public static void ImprimirPeca(Peca peca)
        {
            // se não tem peça, imprime um tracinho
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                // se a cor da peça for branca
                if (peca.Cor == Cor.Branca)
                {
                    // imprime a peça branca na posição dela no tabuleiro
                    Console.Write(peca);
                }
                else
                {
                    // cria uma variável auxiliar para guardar a cor atual (cor padrão do console)
                    ConsoleColor aux = Console.ForegroundColor;
                    // altera a cor atual para amarelo (simboliza a peça preta)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    // imprime a peça preta na posição dela no tabuleiro
                    Console.Write(peca);
                    // volta a cor para o padrão do console
                    Console.ForegroundColor = aux;
                }
                // imprime um espaço em branco depois da peça
                Console.Write(" ");
            }



        }
    }
}
