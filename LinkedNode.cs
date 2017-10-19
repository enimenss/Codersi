using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Graph
{
    public class LinkedNode
    {
       public int Id { get; set; }
       public Edge Adj { get; set; }
       public LinkedNode Next { get; set; }
       public int Status { get; set; }
       public bool inQueue { get; set; }
       public int TeamMate { get; set; }
       public int Level { get; set; }
       public List<int> path = new List<int>();

    }
}