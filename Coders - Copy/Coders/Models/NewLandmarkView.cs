using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class NewLandmarkView
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<int> UserId { get; set; }
        public Guid UsersGuid { get; set; }

        public NewLandmarkView() { }

        public NewLandmarkView(NewLandmark l)
        {
            Id = l.Id;
            Picture = l.Picture;
            Description = l.Description;
            Rate = l.Rate;
            UserId = l.UserId;
        }
    }
}