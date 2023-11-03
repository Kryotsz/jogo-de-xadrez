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
        // conjunto de peças (atributo)
        private HashSet<Peca> Pecas;
        // conjunto de peças capturadas (atributo)
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }
        // propriedade que carrega o estado da peça, se está vulnerável à jogada especial En Passant ou não
        public Peca VulneravelEnPassant { get; private set; }

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
            // inicializa a variável como nulo
            VulneravelEnPassant = null;
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

            // #JogadaEspecial - Roque Pequeno
            // se a peça for um Rei, e vai se mover 2 casas pra direita, então o movimento é um Roque Pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                // cria as variáveis que recebem as posiçoes de origem e destino da Torre
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                // retira a Torre da posição de origem dela
                Peca T = Tabuleiro.RetirarPeca(origemT);
                // incrementa o movimento da Torre
                T.IncrementarQtdeMovimentos();
                // coloca a Torre no destino dela
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #JogadaEspecial - Roque Grande
            // se a peça for um Rei, e vai se mover 2 casas pra esquerda, então o movimento é um Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                // cria as variáveis que recebem as posiçoes de origem e destino da Torre
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                // retira a Torre da posição de origem dela
                Peca T = Tabuleiro.RetirarPeca(origemT);
                // incrementa o movimento da Torre
                T.IncrementarQtdeMovimentos();
                // coloca a Torre no destino dela
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // #jogadaEspecial - En Passant
            if (peca is Peao)
            {
                // se o Peão se deslocou na diagonal (movimento de captura) e NÃO capturou nenhuma peça
                // é porque é uma jogada En Passant
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    // variável que guarda a posição do Peão inimigo
                    Posicao posicaoP;
                    // se o Peão for Branco
                    if (peca.Cor == Cor.Branca)
                    {
                        // a posição será uma linha acima
                        posicaoP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        // a posição será uma linha abaixo
                        posicaoP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    // retira a peça que foi capturada do tabuleiro
                    pecaCapturada = Tabuleiro.RetirarPeca(posicaoP);
                    // adiciona a peça capturada no conjunto de peças capturadas
                    Capturadas.Add(pecaCapturada);
                }
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

            // #JogadaEspecial - Roque Pequeno
            // se a peça for um Rei, e vai se mover 2 casas pra direita, então o movimento é um Roque Pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                // cria as variáveis que recebem as posiçoes de origem e destino da Torre
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                // retira a Torre da posição de destino dela
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                // decrementa o movimento da Torre
                T.DecrementarQtdeMovimentos();
                // coloca a Torre de volta na origem
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // #JogadaEspecial - Roque Grande
            // se a peça for um Rei, e vai se mover 2 casas pra esquerda, então o movimento é um Roque Grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                // cria as variáveis que recebem as posiçoes de origem e destino da Torre
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                // retira a Torre da posição de destino dela
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                // decrementa o movimento da Torre
                T.DecrementarQtdeMovimentos();
                // coloca a Torre de volta na origem
                Tabuleiro.ColocarPeca(T, origemT);
            }

            // #jogadaEspecial En Passant
            if (peca is Peao)
            {
                // se o Peão se deslocou na diagonal (movimento de captura) e a peça capturada estava vulnerável à jogada En Passant
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    // retira o peão capturado do tabuleiro
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posicaoP;
                    // se a peca for branca
                    if (peca.Cor == Cor.Branca)
                    {
                        // a posicao do Peão capturado será à esquerda do Peão Branco
                        posicaoP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        // a posição do Peão capturado será à direita do Peão Preto
                        posicaoP = new Posicao(4, destino.Coluna);
                    }
                    // coloca o Peão que tinha sido capturado de volta no tabuleiro, na posição certa
                    Tabuleiro.ColocarPeca(peao, posicaoP);
                }
            }

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

            // peça que foi movida
            Peca peca = Tabuleiro.Peca(destino);

            // #jogadaEspecial - Promoção
            if (peca is Peao)
            {
                if ((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
                {
                    // a peça que vai ser promovida é tirada do tabuleiro
                    peca = Tabuleiro.RetirarPeca(destino);
                    // a peça é tirada do conjunto de peças em jogo
                    Pecas.Remove(peca);
                    // cria uma nova Dama (rainha), já que o Peão foi promovido
                    Peca dama = new Dama(Tabuleiro, peca.Cor);
                    // coloca a Dama (Rainha) no tabuleiro, no mesmo lugar em que o Peão estava anteriormente
                    Tabuleiro.ColocarPeca(dama, destino);
                    // adiciona a nova Dama (Rainha) no conjunto de peças do tabuleiro
                    Pecas.Add(dama);
                }
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

            // #JogadaEspecial - En Passant
            if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = peca;
            }
            else
            {
                VulneravelEnPassant = null;
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
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
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
                if (x is Rei)
                {
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
            // Parte superior do tabuleiro (peças pretas)
            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            // o "this" se refere a partida, já que estamos dentro dessa classe
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));

            // Parte inferior do tabuleiro (peças brancas)
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            // o "this" se refere a partida, já que estamos dentro dessa classe
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));
        }
    }
}
