using TabuleiroXadrezEnums;

namespace TabuleiroXadrez
{
    internal class Peca
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
    }
}
