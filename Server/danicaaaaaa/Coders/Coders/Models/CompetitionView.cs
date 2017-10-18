using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class CompetitionView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }
        public int Type { get; set; }
        public Nullable<int> LandmarkCount { get; set; }
        public int City { get; set; }
        public System.Guid Guid { get; set; }

        public CompetitionView() { }

        public CompetitionView(Competition c)
        {
            Id = c.Id;
            Name = c.Name;
            StartingDate = c.StartingDate.ToString();
            EndingDate = c.EndingDate.ToString();
            Type = c.Type;
            LandmarkCount = c.LandmarkCount;
            City = c.City;
            Guid = c.Guid;
        }
    }
}