namespace Exceptions
{
    // classe TabuleiroException herda de Exception
    internal class TabuleiroException : Exception
    {
        // construtor da classe
        public TabuleiroException(string msg) : base(msg) { }
    }
}
