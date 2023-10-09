using TabuleiroXadrezEnums;

namespace TabuleiroXadrez
{
    internal class Peca
    {
        public Tabuleiro Tabuleiro { get; protected set; }
        public Cor Cor { get; protected set; }
        public Posicao Posicao { get; set; }
        public int QtdeMovimentos { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            Tabuleiro = tabuleiro;
            Cor = cor;
            Posicao = null;
            QtdeMovimentos = 0;
            
        }
    }
}
