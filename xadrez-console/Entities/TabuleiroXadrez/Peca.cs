using TabuleiroXadrezEnums;

namespace TabuleiroXadrez
{
    internal abstract class Peca
    {
        // propriedades da peça
        public Tabuleiro Tabuleiro { get; protected set; }
        public Cor Cor { get; protected set; }
        public Posicao Posicao { get; set; }
        public int QtdeMovimentos { get; protected set; }

        // construtor de 2 parâmetros
        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Tabuleiro = tabuleiro;
            Cor = cor;
            // a peça começa sem posição
            Posicao = null;
            // a peça começa com 0 movimentos
            QtdeMovimentos = 0;
        }

        // método que incrementa em 1 o movimento da peça
        public void IncrementarQtdeMovimentos()
        {
            QtdeMovimentos++;
        }

        // método que irá percorrer toda matriz de movimentos possíveis e retornar true ou false caso exista ou não um movimento possível
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] matriz = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // método que verifica se a peça pode se mover para a posição recebida por parâmetro
        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        // método abstrato de movimentos possíveis da peça
        public abstract bool[,] MovimentosPossiveis();
    }
}
