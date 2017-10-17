using Coders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Data_Layer
{
    public class DLLandmarks
    {
        private static DLLandmarks data;
        CodersEntities db;

        private DLLandmarks() { }

        public static DLLandmarks Data
        {
            get
            {
                if (data == null)
                {
                    data = new DLLandmarks();
                }
                return data;
            }
        }

        public List<LandmarkView> ReturnLandmarks()
        {
            using (db = new CodersEntities())
            {
                List<Landmark> ladmarks =  (from l in db.Landmarks where l.Deleted == 0 select l).ToList();
                List<LandmarkView> landmarksView = new List<LandmarkView>();
                foreach(Landmark l in ladmarks)
                {
                    landmarksView.Add(new LandmarkView(l));
                }
                return landmarksView;
            }
        }
           
        public int ReturnLandmarkCount()
        {
           return (from l in db.Landmarks where l.Deleted == 0 select l).Count();
        }

        public List<LandmarkView> ReturnLandmark(int skip, int take)
        {
            List<Landmark> ladmarks = (from l in db.Landmarks where l.Deleted == 0 select l).Skip(skip).Take(take).ToList();
            List<LandmarkView> landmarksView = new List<LandmarkView>();
            foreach (Landmark l in ladmarks)
            {
                landmarksView.Add(new LandmarkView(l));
            }
            return landmarksView;
        }
    }
                
}
