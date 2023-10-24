using JogoXadrez;
using System.Data.Common;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace XadrezConsole
{
    internal class Tela
    {
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
                    // se a posição no tabuleiro é igual a nulo, significa que não tem peça lá
                    if (tabuleiro.Peca(i, j) == null)
                    {
                        // então imprime o sinal "-" que significa que não tem peça
                        Console.Write("- ");
                    }
                    else
                    {
                        // chama o método pra imprimir a peça, passa a peça no tabuleiro como parâmetro
                        ImprimirPeca(tabuleiro.Peca(i, j));
                        // imprime um espaço no final
                        Console.Write(" ");
                    }
                }
                // quebra a linha
                Console.WriteLine();
            }
            // imprime a letra referente às colunas na parte debaixo do tabuleiro
            Console.WriteLine("  a b c d e f g h");
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
        }
    }
}
