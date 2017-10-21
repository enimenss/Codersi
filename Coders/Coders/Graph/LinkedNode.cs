using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Graph
{
    public class LinkedNode
    {
        public List<int> path = new List<int>();

        public int Id { get; set; }
       public Edge Adj { get; set; }
       public LinkedNode Next { get; set; }
       public int Status { get; set; }
       public bool inQueue { get; set; }
        public int Level { get; internal set; }
        public int TeamMate { get; internal set; }
    }
}