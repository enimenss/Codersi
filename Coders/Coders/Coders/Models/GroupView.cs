using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class GroupView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> CountOfFounds { get; set; }
        public System.Guid Guid { get; set; }

        public GroupView() { }

        public GroupView(Group g)
        {
            Id = g.Id;
            Name = g.Name;
            CountOfFounds = g.CountOfFounds;
            Guid = g.Guid;
        }
    }
}