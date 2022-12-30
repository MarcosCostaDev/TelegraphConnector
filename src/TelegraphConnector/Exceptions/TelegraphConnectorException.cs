namespace TelegraphConnector.Exceptions
{
    public class TelegraphConnectorException : Exception
    {
        protected TelegraphConnectorException() { }
        public TelegraphConnectorException(string message) : base(message) { }
        public TelegraphConnectorException(string message, Exception innerException) : base(message, innerException) { }

    }
}
