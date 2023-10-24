using Exceptions;

namespace TabuleiroXadrez
{
    internal class Tabuleiro
    {
        // quantidade de linhas do tabuleiro
        public int Linhas { get; set; }
        // quantidade de colunas do tabuleiro
        public int Colunas { get; set; }
        // matriz de peças que estão no tabuleiro
        private Peca[,] Pecas;

        // construtor que recebe linhas e colunas
        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        // método Peca que recebe linha e coluna
        public Peca Peca(int linha, int coluna)
        {
            // coloca a peça na matriz de peças do tabuleiro utilizando somente linha e coluna
            return Pecas[linha, coluna];
        }

        // método Peca que recebe a posição
        // coloca a peça na matriz de peças do tabuleiro
        public Peca Peca(Posicao posicao)
        {
            // coloca a peça na matriz de peças do tabuleiro utilizando a posição
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        // método que verifica a existência de peça na posição recebida por parâmetro
        public bool ExistePeca(Posicao posicao)
        {
            // chama o método ValidarPosicao e manda por parâmetro a posição da peça
            ValidarPosicao(posicao);
            // retorna True se existir uma peça naquela posição
            return Peca(posicao) != null;
        }

        // método para colocar a peça no tabuleiro, recebe a peça e a posição por parâmetro
        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            // chama o método ExistePeça e passa a posição por parâmetro
            if (ExistePeca(posicao))
            {
                // se for True, lança uma nova exceção dizendo que já existe uma peça nessa posição
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            // se não existe, a matriz de peças do tabuleiro recebe essa peça
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            // e a posição da peça passa a ser essa posição válida
            peca.Posicao = posicao;
        }

        // método para retirar peça do tabuleiro, recebe a posição por parâmetro
        public Peca RetirarPeca(Posicao posicao)
        {
            // verifica se a peça NÃO existe no tabuleiro
            if (Peca(posicao) == null)
            {
                // se NÃO existe, retorna null
                return null;
            }
            // se a peça existe
            // cria uma variável auxiliar que guarda a posição atual da peça
            Peca aux = Peca(posicao);
            // substitui a posição atual da peça por nulo
            aux.Posicao = null;
            // tira a peça da matriz de peças do tabuleiro
            Pecas[posicao.Linha, posicao.Coluna] = null;
            // retorna a variável auxiliar
            return aux;
        }

        // método que verifica se a posição recebida por parâmetro é uma posição válida (existe no tabuleiro)
        public bool PosicaoValida(Posicao posicao)
        {
            // se a linha ou a coluna da posição estiver fora dos limites do tabuleiro, retorna False
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
            {
                return false;
            }
            // se for uma posição válida (dentro dos limites do tabuleiro) retorna True
            return true;
        }

        // método que valida a posição, recebe por parâmetro a posição de uma peça
        // se a posição nao for válida, retorna uma exceção
        public void ValidarPosicao(Posicao posicao)
        {
            // chama o método PosicaoValida e passa a posição como parâmetro
            // se retornar False
            if (!PosicaoValida(posicao))
            {
                // lança uma nova exceção, passando a mensagem de Posição Inválida
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
