using JogoXadrez;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace XadrezConsole
{
    internal class Tela
    {
        // método para imprimir a partida na tela
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            // chama o método para imprimir o tabuleiro
            ImprimirTabuleiro(partida.Tabuleiro);
            Console.WriteLine();
            // imprime os conjuntos de peças capturadas na tela
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            // imprime o turno atual
            Console.WriteLine($"Turno: {partida.Turno}");
            // se a partida NÃO terminou
            if (!partida.Terminada)
            {
                // imprime de quem é a vez
                Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                // verifica se um xeque ocorreu
                if (partida.Xeque)
                {
                    // avisa o jogador que houve o xeque
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                // imprime na tela o fim da partida, indicando qual jogador ganhou
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
            }
        }

        // método que imprime na tela o conjunto de peças capturadas de ambas as cores
        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            // imprime na tela o conjunto de peças brancas capturadas
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            // imprime na tela o conjunto de peças pretas capturadas
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
            // imprime um conjunto de peças dentro de colchetes
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
