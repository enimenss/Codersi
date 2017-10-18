using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coders.Models;

namespace Coders.Data_Layer
{
    public class DLCompetition
    {
        private static DLCompetition data;
        CodersEntities db;

        private DLCompetition() { }

        public static DLCompetition Data
        {
            get
            {
                if (data == null)
                {
                    data = new DLCompetition();
                }
                return data;
            }
        }

        public List<CompetitionView> ReturnCompetitions()
        {
            using (db = new CodersEntities())
            {
                List<Competition> competitions = (from u in db.Competitions select u).ToList();
                List<CompetitionView> competitionsView = new List<CompetitionView>();
                foreach (Competition c in competitions)
                {
                    competitionsView.Add(new CompetitionView(c));
                }
                return competitionsView;
            }
        }

        public int ReturnCompetitionCount()
        {
            using (db = new CodersEntities())
            {
                return (from c in db.Competitions select c).Count();
            }
        }

        public List<CompetitionView> ReturnCompetitions(int skip, int take)
        {
            using (db = new CodersEntities())
            {
                List<Competition> competitions = (from c in db.Competitions select c).OrderBy(x => x.Id).Skip(skip).Take(take).ToList();
                List<CompetitionView> competitionsView = new List<CompetitionView>();
                foreach (Competition c in competitions)
                {
                    competitionsView.Add(new CompetitionView(c));
                }
                return competitionsView;
            }
        }

        public void DeleteCompetition(int id)
        {
            using (db = new CodersEntities())
            {
                Competition competition = db.Competitions.Find(id);
                db.Competitions.Attach(competition);
                //competition.Deleted = 1;
                db.SaveChanges();
            }
        }

        public void ChangeCompetition(CompetitionView competitionView)
        {
            using (db = new CodersEntities())
            {
                Competition competition = db.Competitions.Find(competitionView.Id);
                if(competition != null)
                {
                    db.Competitions.Attach(competition);
                    competition.LandmarkCount = competitionView.LandmarkCount;
                    competition.Name = competitionView.Name;
                    competition.StartingDate = DateTime.Parse(competitionView.StartingDate);
                    competition.Type = competitionView.Type;
                    competition.EndingDate = DateTime.Parse(competitionView.EndingDate);
                    competition.City = competitionView.City;

                    db.SaveChanges();
                }
            }
        }
    }
}