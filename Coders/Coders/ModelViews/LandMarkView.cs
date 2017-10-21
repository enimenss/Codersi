using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.ModelViews
{
    public class LandMarkView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int City { get; set; }
        public string FilePathLandmark { get; set; }
        public string FilePath { get; set; }
        public double Lat { get; internal set; }
        public double Lon { get; internal set; }
    }
}