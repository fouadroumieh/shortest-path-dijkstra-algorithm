using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingNetworkLibrary
{
    class Edge
    {
        public int u;  // source vertex
        public int v;  // target vertex
        public int w;  // weight

        public Edge(int u, int v, int w)
        {
            this.u = u;
            this.v = v;
            this.w = w;
        }
    }
}
