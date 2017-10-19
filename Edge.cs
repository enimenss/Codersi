using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Graph
{
    public class Edge
    {
        public LinkedNode Dest { get; set; }
        public Edge Link { get; set; }
    }
}