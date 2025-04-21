namespace Dg.Pays.Domain.Exceptions
{
    public class InvalidCardExpirationException : Exception
    {
        public InvalidCardExpirationException(string message) : base(message) { }
    }
}
