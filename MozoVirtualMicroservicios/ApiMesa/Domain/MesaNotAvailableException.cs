namespace ApiMesa.Domain
{
    public class MesaNotAvailableException : Exception
    {
        public MesaNotAvailableException(string? message) : base(message)
        {
        }

        public MesaNotAvailableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
