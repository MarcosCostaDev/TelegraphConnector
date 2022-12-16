using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegraphConnector.Types
{
    public class NodeElement : Node
    {
        public string Tag { get; private set; }
        public IDictionary<string, string> Attrs { get; private set; }
        public IEnumerable<Node> Children { get; private set; }
    }
}
