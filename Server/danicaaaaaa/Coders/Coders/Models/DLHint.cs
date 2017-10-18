using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Models
{
    public class DLHint
    {
        private static DLHint data;
        CodersEntities db;

        private DLHint() { }

        public static DLHint Data
        {
            get
            {
                if (data == null)
                {
                    data = new DLHint();
                }
                return data;
            }
        }

        public List<Hint> ReturnHints(int id)
        {
            using (db = new CodersEntities())
            {
                return (from h in db.Hints where h.Landmark_Id == id select h).ToList();
            }
        }
    }
}