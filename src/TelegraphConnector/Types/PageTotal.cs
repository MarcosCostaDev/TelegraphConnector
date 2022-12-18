using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Types
{
    public class PageTotal : AbstractTypes
    {
        protected PageTotal() { }

        [JsonProperty("total_count")]
        public int TotalCount { get; private set; }
        public IEnumerable<Page> Pages { get; private set; }

    }
}
