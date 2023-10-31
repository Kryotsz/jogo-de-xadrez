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
        // conjunto de peças
        private HashSet<Peca> Pecas;
        // conjunto de peças capturadas
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            // cria o tabuleiro com esse tamanho
            Tabuleiro = new Tabuleiro(8, 8);
            // começa no turno 1
            Turno = 1;
            // jogador das peças brancas começa o jogo
            JogadorAtual = Cor.Branca;
            // a partida não está terminada
            Terminada = false;
            // o Rei não está em xeque
            Xeque = false;
            // instancia o conjunto de peças do tabuleiro
            Pecas = new HashSet<Peca>();
            // instancia o conjunto de peças capturadas
            Capturadas = new HashSet<Peca>();
            // chama o método para colocar as peças no tabuleiro
            ColocarPecas();
        }

        // método que executa o movimento da peça
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
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
            return pecaCapturada;
        }

        // método para desfazer o movimento de uma peça
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            // tira a peça da posição de destino
            Peca peca = Tabuleiro.RetirarPeca(destino);
            // chama o método para decrementar o movimento dessa peça
            peca.DecrementarQtdeMovimentos();
            // se uma peça foi capturada nesse movimento
            if (pecaCapturada != null)
            {
                // coloca a peça capturada de volta no jogo, na posição em que ela estava
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                // tira a peça capturada do conjunto de peças capturadas
                Capturadas.Remove(pecaCapturada);
            }
            // coloca a peça de volta na posição de origem dela
            Tabuleiro.ColocarPeca(peca, origem);
        }

        // método para realizar a jogada do jogador
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            // chama o método para executar o movimento, passando as posições de origem e destino da peça
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            // se o movimento realizado pelo jogador atual o colocar em xeque
            if (EstaEmXeque(JogadorAtual))
            {
                // chama o método para desfazer esse movimento, passando por parâmetro a origem e destino da peça movida, juntamente com uma possível peça capturada
                DesfazMovimento(origem, destino, pecaCapturada);
                // e lança um erro
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            // se o movimento realizado pelo jogador atual, colocar o adversário em xeque
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                // altera a variável Xeque para true
                Xeque = true;
            }
            else
            {
                // altera para false (continua false)
                Xeque = false;
            }

            // se tiver em Xeque-mate
            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                // a partida termina
                Terminada = true;
            }
            else
            {
                // passa o turno
                Turno++;
                // troca pro outro jogador
                MudaJogador();
            }
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

        // método pra retornar a cor adversária do jogador atual
        private Cor Adversaria(Cor cor)
        {
            // se a cor recebida por parâmetro for igual a branca
            if (cor == Cor.Branca)
            {
                // retorna preta (cor adversária)
                return Cor.Preta;
            }
            else
            {
                // retorna branca (cor adversária)
                return Cor.Branca;
            }
        }

        // método para retornar o rei da cor recebida por parâmetro
        private Peca Rei(Cor cor)
        {
            // percorre o conjunto de peças em jogo daquela cor
            foreach (Peca x in PecasEmJogo(cor))
            {
                // se a peça for um rei
                if (x is Rei){
                    // retorna o rei
                    return x;
                }
            }
            // retorna null caso não encontre nenhum rei em jogo
            return null;
        }

        // método que verifica se o Rei está em xeque
        public bool EstaEmXeque(Cor cor)
        {
            // cria a variável R que recebe o rei desta cor
            Peca R = Rei(cor);
            // se não tiver rei no jogo
            if (R == null)
            {
                // lança um erro
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
            }
            // percorre o conjunto de peças adversárias em jogo
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                // cria uma matriz com os movimentos possíveis de cada peça
                bool[,] matriz = x.MovimentosPossiveis();
                // se algum movimento possível for igual a posição do Rei inimigo
                if (matriz[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    // Rei inimigo está em xeque
                    return true;
                }
            }
            // Rei inimigo não está em xeque
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            // se o rei dessa cor não está em Xeque, é impossível estar em Xeque-mate
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] matriz = x.MovimentosPossiveis();
                // for encadeado para percorrer toda a matriz
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        // se a matriz nessa posição estiver marcada como verdadeiro, significa que é um movimento possível
                        if (matriz[i, j])
                        {
                            // cria a variável origem para armazenar a posição de origem da peça
                            Posicao origem = x.Posicao;
                            // cria a variável destino com a posição possível encontrada
                            Posicao destino = new Posicao(i, j);
                            // movimenta a peça x para a posição possível encontrada
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            // verifica se após esse movimento, o Rei ainda está em xeque
                            bool testeXeque = EstaEmXeque(cor);
                            // desfaz esse movimento da peça
                            DesfazMovimento(origem, destino, pecaCapturada);
                            // verifica se testeXeque é falso, se for, o jogo continua
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            // se nenhum movimento que salva o Rei foi encontrado através do loop
            // o Rei está em Xeque-mate
            return true;
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
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(Tabuleiro, Cor.Branca));

            ColocarNovaPeca('a', 8, new Rei(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Torre(Tabuleiro, Cor.Preta));
        }
    }
}
