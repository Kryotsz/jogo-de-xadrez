namespace TabuleiroXadrez
{
    internal class Posicao
    {
        // propriedades da posição
        public int Linha { get; set; }
        public int Coluna { get; set; }

        // construtor de 2 parâmetros
        public Posicao(int linha, int coluna) 
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        // imprime a linha e coluna na tela
        public override string ToString()
        {
            return Linha
                + ", "
                + Coluna;
        }
    }
}
