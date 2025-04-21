namespace Dg.Pays.Domain.Exceptions
{
    public class InvalidCardNumberException : Exception
    {
        public InvalidCardNumberException(string message) : base(message) { }
    }
}
