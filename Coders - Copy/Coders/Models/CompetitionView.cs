using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Coders.Enums;

namespace Coders.Models
{
    public class CompetitionView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartingDate { get; set; }
        public string EndingDate { get; set; }
        public Types Type { get; set; }
        public Nullable<int> LandmarkCount { get; set; }
        public Cities City { get; set; }
        public System.Guid Guid { get; set; }

        public CompetitionView() { }

        public CompetitionView(Competition c)
        {
            Id = c.Id;
            Name = c.Name;
            StartingDate = c.StartingDate.ToString("yyyy-MM-dd");
            EndingDate = c.EndingDate.ToString("yyyy-MM-dd");
            Type = (Types) c.Type;
            LandmarkCount = c.LandmarkCount;
            City = (Cities) c.City;
            Guid = c.Guid;
        }
    }
}