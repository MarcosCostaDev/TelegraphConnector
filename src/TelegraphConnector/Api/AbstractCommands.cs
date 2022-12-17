using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Api
{
    public abstract class AbstractCommands 
    {
        protected readonly ITelegraphClient _telegraphClient;
        protected readonly CancellationToken _cancellationToken;
        public AbstractCommands(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null)
        {
            _telegraphClient = telegraphClient ?? new TelegraphClient();
            _cancellationToken = cancellationToken ?? CancellationToken.None;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver()
            };
        }

        protected StringContent GetJsonContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}
