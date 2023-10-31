using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class Rei : Peca
    {
        // propriedade partida (para o movimento Roque ser possível)
        // a classe Rei precisa "enxergar" a partida, para ver se está em Xeque ou não
        private PartidaXadrez Partida;

        // construtor do Rei
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor) 
        {
            Partida = partida;
        }

        // método para imrpimir o Rei no tabuleiro
        public override string ToString()
        {
            return "R";
        }

        // método que verifica se a peça pode ser movida
        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            // verifica se não tem nenhuma peça ou se tem uma peça inimiga na posicao desejada
            return peca == null || peca.Cor != Cor;
        }

        // método que verifica se alguma Torre pode fazer o movimento especial Roque
        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            // só poderá fazer o Roque, se a peça existir, se for uma Torre, se for da mesma cor do Rei e se não tiver se movimentado nenhuma vez
            return peca != null && peca is Torre && peca.Cor == Cor && peca.QtdeMovimentos == 0;
        }

        // método para implementar os movimentos possíveis do Rei
        public override bool[,] MovimentosPossiveis()
        {
            // matriz de movimentos possíveis
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // verifica acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica diagonal cima direita
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica diagonal direita baixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica diagonal baixo esquerda
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // verifica diagonal esquerda cima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            // #JogadaEspecial - Roque
            // se o Rei não fez nenhum movimento e não está em xeque
            if (QtdeMovimentos == 0 && !Partida.Xeque)
            {
                // #JogadaEspecial - Roque Pequeno
                // posição que deveria estar a Torre
                Posicao posicaoT1 = new Posicao(posicao.Linha, posicao.Coluna + 3);
                if (TesteTorreParaRoque(posicaoT1))
                {
                    // cria variáveis para receberem as 2 posições à direita do Rei
                    Posicao posicao1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao posicao2 = new Posicao(posicao.Linha, posicao.Coluna + 2);
                    // verifica se as posições à direita do Rei estão vazias
                    if (Tabuleiro.Peca(posicao1) == null && Tabuleiro.Peca(posicao2) == null)
                    {
                        // essa posição na matriz de movimentos possíveis recebe true
                        matriz[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }

                // #JogadaEspecial - Roque Grande
                // posição que deveria estar a Torre
                Posicao posicaoT2 = new Posicao(posicao.Linha, posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoT1))
                {
                    // cria variáveis para receberem as 3 posições à esquerda do Rei
                    Posicao posicao1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    Posicao posicao2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                    Posicao posicao3 = new Posicao(posicao.Linha, posicao.Coluna - 3);
                    // verifica se as posições à esquerda do Rei estão vazias
                    if (Tabuleiro.Peca(posicao1) == null && Tabuleiro.Peca(posicao2) == null && Tabuleiro.Peca(posicao3) == null)
                    {
                        // essa posição na matriz de movimentos possíveis recebe true
                        matriz[posicao.Linha, posicao.Coluna - 2] = true;
                    }
                }
            }

            // retorna a matriz de movimentos possíveis
            return matriz;
        }
    }
}
