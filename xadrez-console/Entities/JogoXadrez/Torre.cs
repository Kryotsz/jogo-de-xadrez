using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) { }

        public override string ToString()
        {
            return "T";
        }

        // método que verifica se a peça pode ser movida
        private bool PodeMover(Posicao posicao)
        {
            Peca peca = Tabuleiro.Peca(posicao);
            // verifica se não tem nenhuma peça ou se tem uma peça inimiga na posicao desejada
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            // matriz de movimentos possíveis
            bool[,] matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao posicao = new Posicao(0, 0);

            // verifica acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            // enquanto a posição estiver dentro dos limites do tabuleiro e não tiver peças do mesmo time impedindo o movimento
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                // passa para a matriz que o movimento é possível
                matriz[posicao.Linha, posicao.Coluna] = true;
                // verifica se tem uma peça inimiga na posição desejada
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                // vai pra linha anterior
                posicao.Linha -= 1;
            }

            // verifica direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                // vai pra próxima coluna
                posicao.Coluna += 1;
            }

            // verifica abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                // vai pra próxima linha
                posicao.Linha += 1;
            }

            // verifica esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (Tabuleiro.Peca(posicao) != null && Tabuleiro.Peca(posicao).Cor != Cor)
                {
                    break;
                }
                // vai pra coluna anterior
                posicao.Coluna -= 1;
            }
            // retorna a matriz de movimentos possíveis
            return matriz;
        }
    }
}
