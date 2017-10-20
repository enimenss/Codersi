using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Data_Layer
{
    public class NewLandmarkView
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public Nullable<int> Rate { get; set; }
        public Nullable<int> UserId { get; set; }
        public Guid UsersGuid { get; set; }
    }
}