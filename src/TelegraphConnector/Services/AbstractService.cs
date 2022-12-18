using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Services
{
    public abstract class AbstractService 
    {
        protected readonly ITelegraphClient _telegraphClient;
        protected readonly CancellationToken _cancellationToken;
        public AbstractService(ITelegraphClient? telegraphClient = null, CancellationToken? cancellationToken = null)
        {
            _telegraphClient = telegraphClient ?? new TelegraphClient();
            _cancellationToken = cancellationToken ?? CancellationToken.None;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver()
                {
                     NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        protected StringContent GetJsonContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return GetStringContent(json);
        }

        protected StringContent GetStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

    }
}
