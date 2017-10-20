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
            using (db = new CodersEntities())
            {
                return (from l in db.Landmarks where l.Deleted == 0 || l.Deleted == null select l).ToList().Count();
            }
        }

        public List<LandmarkView> ReturnLandmarks(int skip, int take)
        {
            using (db = new CodersEntities())
            {
                List<Landmark> ladmarks = (from l in db.Landmarks where l.Deleted == 0 select l).OrderBy(x=>x.Id).Skip(skip).Take(take).ToList();
                List<LandmarkView> landmarksView = new List<LandmarkView>();
                foreach (Landmark l in ladmarks)
                {
                    landmarksView.Add(new LandmarkView(l));
                }
                return landmarksView;
            }
        }

        public void DeleteLandmark(int id)
        {
            using (db = new CodersEntities())
            {
                Landmark landmark = db.Landmarks.Find(id);
                db.Landmarks.Attach(landmark);
                landmark.Deleted = 1;
                db.SaveChanges();
            }
        }

        public void ChangeLandmark(LandmarkView landmarkView)       //nije dobroooooo mora dorada!!!!!
        {
            using (db = new CodersEntities())
            {
                Landmark landmark = db.Landmarks.Find(landmarkView.Id);
                if (landmark != null)
                {
                    db.Landmarks.Attach(landmark);
                    //landmark.City = landmarkView.City;
                    landmark.Name = landmarkView.Name;

                    db.SaveChanges();
                }
            }
        }

        public void AddLandmark(LandmarkView landmarkView)
        {
            using (db = new CodersEntities())
            {
                Landmark landmark = new Landmark();
                landmark.City = (int) landmarkView.City;
                landmark.Name = landmarkView.Name;
                landmark.Deleted = 0;
                landmark.FilePathLandmark = landmarkView.FilePathLandmark;
                db.Landmarks.Add(landmark);
                db.SaveChanges();
            }
        }

        public void UserAddLandmark(NewLandmarkView newLandmarkView)
        {
            using (db = new CodersEntities())
            {
                NewLandmark landmark = new NewLandmark();
                landmark.Description = newLandmarkView.Description;
                landmark.Picture = newLandmarkView.Picture;
                landmark.Id = 0;
                landmark.UserId = DLUser.Data.FindUsersId(newLandmarkView.UsersGuid);
                //landmark.Date = DateTime.Now;
                db.NewLandmarks.Add(landmark);
                db.SaveChanges();
            }
        }
    }
                
}
