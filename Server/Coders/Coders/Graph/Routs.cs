using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Graph
{
    public class Routs
    {
        public int ParentNode { get; set; }
        public int ChildNode { get; set; }
        public List<int> path { get; set; }
    }
}