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
        // conjuntos
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaXadrez()
        {
            // cria o tabuleiro com esse tamanho
            Tabuleiro = new Tabuleiro(8, 8);
            // começa no turno 1
            Turno = 1;
            // jogador das peças brancas começa o jogo
            JogadorAtual = Cor.Branca;
            // instancia o conjunto de peças do tabuleiro
            Pecas = new HashSet<Peca>();
            // instancia o conjunto de peças capturadas
            Capturadas = new HashSet<Peca>();
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
            // se existe uma peça na posição de destino
            if (pecaCapturada != null)
            {
                // adiciona a peça no conjunto de peças capturadas
                Capturadas.Add(pecaCapturada);
            }
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

        // método para retornar o conjunto de peças capturadas de uma determinada cor
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            // cria um conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();
            // percorre todas as peças capturadas
            foreach (Peca x in Capturadas)
            {
                // se a cor da peça for a mesma cor recebida por parâmetro
                if (x.Cor == cor)
                {
                    // adiciona no conjunto aux
                    aux.Add(x);
                }
            }
            // retorna o conjunto aux
            return aux;
        }

        // método que retorna o conjunto de peças em jogo de determinada cor
        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            // cria um conjunto auxiliar
            HashSet<Peca> aux = new HashSet<Peca>();
            // percorre todas as peças capturadas
            foreach (Peca x in Pecas)
            {
                // se a cor da peça for a mesma cor recebida por parâmetro
                if (x.Cor == cor)
                {
                    // adiciona no conjunto aux
                    aux.Add(x);
                }
            }
            // retira as peças do conjunto aux, que forem da mesma cor recebida por parâmetro
            // ou seja, o conjunto aux vai ficar somente com as peças dessa cor que NÃO foram capturadas
            aux.ExceptWith(PecasCapturadas(cor));
            // retorna o conjunto aux
            return aux;
        }

        // método para colocar uma nova peça
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            // chama o método para colocar a peça no tabuleiro, na posição especificada
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            // adiciona a peça no conjunto de peças disponíveis
            Pecas.Add(peca);
        }

        // método para colocar as peças no tabuleiro
        public void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tabuleiro, Cor.Preta));
        }
    }
}
