using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class LandmarkView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int City { get; set; }
        public string FilePath { get; set; }
        public Nullable<double> Location_X { get; set; }
        public Nullable<double> Location_Y { get; set; }
        public string FilePathLandmark { get; set; }
        public Nullable<int> Deleted { get; set; }
        public List<Hint> Hints { get; set; }

        public LandmarkView() { }

        public LandmarkView(Landmark l)
        {
            Id = l.Id;
            Name = l.Name;
            City = l.City;
            FilePath = l.FilePath;
            Location_X = l.Location_X;
            Location_Y = l.Location_Y;
            FilePathLandmark = l.FilePathLandmark;
            Deleted = l.Deleted;

            Hints = DLHint.Data.ReturnHints(Id);
        }
    }
}