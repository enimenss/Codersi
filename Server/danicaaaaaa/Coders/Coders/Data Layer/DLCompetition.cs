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
            return (from c in db.Competitions select c).Count();
        }

        public List<CompetitionView> ReturnCompetitions(int skip, int take)
        {
            List<Competition> competitions = (from c in db.Competitions select c).Skip(skip).Take(take).ToList();
            List<CompetitionView> competitionsView = new List<CompetitionView>();
            foreach (Competition c in competitions)
            {
                competitionsView.Add(new CompetitionView(c));
            }
            return competitionsView;
        }
    }
}