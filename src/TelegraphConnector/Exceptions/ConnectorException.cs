using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Exceptions
{
    public class ConnectorException : Exception
    {
        public ConnectorException(string message)
        {
            ConnectorMessage = message;
        }

        public string ConnectorMessage { get; }
    }
}
