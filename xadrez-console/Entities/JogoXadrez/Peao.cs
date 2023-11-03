using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class Peao : Peca
    {
        // atributo para o Peão enxergar a partida, possibilitanto enxergar as peças vulneráveis à jogada En Passant
        private PartidaXadrez Partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor) 
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        // método que verifica se existe uma peça adversária na posição desejada
        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca != null && peca.Cor != Cor;
        }

        // método que verifica se a posição está livre
        private bool Livre(Posicao posicao)
        {
            return Tabuleiro.Peca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                // 1 casa pra cima
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // 2 casas pra cima
                posicao.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QtdeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // cima esquerda (caso exista inimigo)
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // cima direita (caso exista inimigo)
                posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaEspecial - En Passant
                // se o Peão Branco está na única linha que pode fazer o En Passant
                if (Posicao.Linha == 3)
                {
                    // variável que guarda a posição da esquerda do Peão
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    // se a posição à esquerda do Peão Branco é válida, se existe inimigo e se o inimigo está vulnerável à jogada En Passant
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    // variável que guarda a posição da direita do Peão
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    // se a posição à direita do Peão Branco é válida, se existe inimigo e se o inimigo está vulnerável à jogada En Passant
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                // 1 casa pra baixo
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // 2 casas pra baixo
                posicao.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QtdeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // baixo esquerda (caso exista inimigo)
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // baixo direita (caso exista inimigo)
                posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // #jogadaEspecial - En Passant
                // se o Peão Preto está na única linha que pode fazer o En Passant
                if (Posicao.Linha == 4)
                {
                    // variável que guarda a posição da esquerda do Peão
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    // se a posição à esquerda do Peão Preto é válida, se existe inimigo e se o inimigo está vulnerável à jogada En Passant
                    if (Tabuleiro.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tabuleiro.Peca(esquerda) == Partida.VulneravelEnPassant)
                    {
                        matriz[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    // variável que guarda a posição da direita do Peão
                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    // se a posição à direita do Peão Preto é válida, se existe inimigo e se o inimigo está vulnerável à jogada En Passant
                    if (Tabuleiro.PosicaoValida(direita) && ExisteInimigo(direita) && Tabuleiro.Peca(direita) == Partida.VulneravelEnPassant)
                    {
                        matriz[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }

            return matriz;
        }
    }
}
