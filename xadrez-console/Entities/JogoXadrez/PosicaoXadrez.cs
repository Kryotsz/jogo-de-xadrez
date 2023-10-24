using TabuleiroXadrez;

namespace JogoXadrez
{
    internal class PosicaoXadrez
    {
        // propriedades da posição
        public char Coluna { get; set; }
        public int Linha { get; set; }

        // construtor de 2 parâmetros
        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        // método que cria uma nova posição
        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        // método que imprime na tela a posição
        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
