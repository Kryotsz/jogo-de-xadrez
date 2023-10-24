using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class PartidaXadrez
    {
        // propriedades da partida de xadrez
        public Tabuleiro Tabuleiro { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        // propriedade que determina se a partida terminou ou não
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            // cria o tabuleiro com esse tamanho
            Tabuleiro = new Tabuleiro(8, 8);
            // começa no turno 1
            Turno = 1;
            // jogador das peças brancas começa o jogo
            JogadorAtual = Cor.Branca;
            // chama o método para colocar as peças no tabuleiro
            ColocarPecas();
        }

        // método que executa o movimento da peça
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            // peça que vai ser movimentada é retirada da origem
            Peca peca = Tabuleiro.RetirarPeca(origem);
            // incrementa a quantidade de movimentos da peça
            peca.IncrementarQtdeMovimentos();
            // captura a peça adversária
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            // coloca a peça que foi movimentada na posição de destino
            Tabuleiro.ColocarPeca(peca, destino);
        }

        public void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());

            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Tabuleiro, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Rei(Tabuleiro, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
        }
    }
}
