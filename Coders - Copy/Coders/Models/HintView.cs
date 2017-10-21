using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class HintView
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public Nullable<int> Landmark_Id { get; set; }

        public HintView() { }

        public HintView(Hint h)
        {
            Id = h.Id;
            Description = h.Description;
            Answer = h.Answer;
            Landmark_Id = h.Landmark_Id;
        }
    }
}