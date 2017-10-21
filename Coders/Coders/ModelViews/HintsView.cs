using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.ModelViews
{
    public class HintsView
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public int? Radius { get; internal set; }
    }
}