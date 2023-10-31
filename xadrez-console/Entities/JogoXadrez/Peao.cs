using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

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
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // 2 casas pra cima
                posicao.DefinirValores(posicao.Linha - 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QtdeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // cima esquerda (caso exista inimigo)
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // cima direita (caso exista inimigo)
                posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }
            else
            {
                // 1 casa pra baixo
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // 2 casas pra baixo
                posicao.DefinirValores(posicao.Linha + 2, posicao.Coluna);
                if (Tabuleiro.PosicaoValida(posicao) && Livre(posicao) && QtdeMovimentos == 0)
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // baixo esquerda (caso exista inimigo)
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }

                // baixo direita (caso exista inimigo)
                posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (Tabuleiro.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    matriz[posicao.Linha, posicao.Coluna] = true;
                }
            }

            return matriz;
        }
    }
}
