using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.ModelViews
{
    public class NewLandmarksView
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public int Rate { get; set; }
    }
}