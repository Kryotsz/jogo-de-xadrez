using Exceptions;
using TabuleiroXadrez;
using TabuleiroXadrezEnums;

namespace JogoXadrez
{
    internal class PartidaXadrez
    {
        // propriedades da partida de xadrez
        public Tabuleiro Tabuleiro { get; private set; }
        // guarda o turno atual
        public int Turno { get; private set; }
        // guarda o jogador que joga no momento
        public Cor JogadorAtual { get; private set; }
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        // método para validar a posição de origem da peça
        public void ValidarPosicaoDeOrigem(Posicao posicao)
        {
            // verifica se existe uma peça naquela posição
            if (Tabuleiro.Peca(posicao) == null)
            {
                // retorna um erro
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            // verifica se a cor da peça escolhida é diferente da cor do jogador atual
            if (JogadorAtual != Tabuleiro.Peca(posicao).Cor)
            {
                // retorna um erro
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            // verifica se a peça escolhida tem movimentos possíveis
            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
            {
                // retorna um erro
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        // método para validar a posição de destino da peça
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            // se a peça na posição de origem não pode mover para a posição de destino
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                // retorna um erro
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        // método que troca o jogador que vai jogar o próximo turno
        private void MudaJogador() 
        {
            // se o jogador atual era as peças brancas
            if (JogadorAtual == Cor.Branca)
            {
                // o próximo será as peças pretas
                JogadorAtual = Cor.Preta;
            }
            else
            {
                // o próximo será as peças brancas
                JogadorAtual = Cor.Branca;
            }
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
