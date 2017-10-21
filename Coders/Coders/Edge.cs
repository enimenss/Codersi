using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodersJoca.Graph
{
    public class Edge
    {
        public LinkedNode Dest { get; set; }
        public Edge Link { get; set; }
        public List<int> path = new List<int>();
    }
}