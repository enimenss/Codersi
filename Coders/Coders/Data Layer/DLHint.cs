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

        public List<HintView> ReturnHints(int id)
        {
            using (db = new CodersEntities())
            {
                List<Hint> hints = (from h in db.Hints where h.Landmark_Id == id select h).ToList();
                List<HintView> hintsView = new List<HintView>();
                foreach(Hint h in hints)
                {
                    hintsView.Add(new HintView(h));
                }
                return hintsView;
            }
        }

        public int ReturnHintsCount(int id)
        {
            using (db = new CodersEntities())
            {
                return (from h in db.Hints where h.Landmark_Id == id select h).ToList().Count;
            }
        }

        public List<HintView> ReturnHints(int id, int skip, int take)
        {
            using (db = new CodersEntities())
            {
                List<Hint> hints = (from h in db.Hints where h.Landmark_Id == id select h).OrderBy(x=>x.Id).Skip(skip).Take(take).ToList();
                List<HintView> hintsView = new List<HintView>();
                foreach (Hint h in hints)
                {
                    hintsView.Add(new HintView(h));
                }
                return hintsView;
            }
        }

        public void ChangeHint(HintView hintView)
        {
            using (db = new CodersEntities())
            {
                Hint hint = db.Hints.Find(hintView.Id);
                if(hint != null)
                {
                    db.Hints.Attach(hint);
                    hint.Description = hintView.Description;
                    hint.Answer = hintView.Answer;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteHint(int id)
        {
            using (db = new CodersEntities())
            {
                Hint hint = db.Hints.Find(id);
                if(hint != null)
                {
                    //hint.Deleted = 1;
                    db.SaveChanges();
                }
            }
        }

        public void AddHint(HintView hintView)
        {
            using (db = new CodersEntities())
            {
                Hint hint = new Hint();
                hint.Description = hintView.Description;
                hint.Answer = hintView.Answer;
                hint.Landmark_Id = hintView.Landmark_Id;
                db.Hints.Add(hint);
                db.SaveChanges();
            }
        }
    }
}