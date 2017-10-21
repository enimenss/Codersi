using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coders.Models;
using System.Data.Entity;

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

        public void AddSolution(Solution s)
        {
            using (db = new CodersEntities())
            {
                db.Solutions.Add(s);
                db.SaveChanges();
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
                    competition.Type = (int) competitionView.Type;
                    competition.EndingDate = DateTime.Parse(competitionView.EndingDate);
                    competition.City = (int) competitionView.City;

                    db.SaveChanges();
                }
            }
        }

        public void AddCompetition(CompetitionView competitionView)
        {
            using (db = new CodersEntities())
            {
                Competition competition = new Competition();
                competition.LandmarkCount = competitionView.LandmarkCount;
                competition.Name = competitionView.Name;
                competition.StartingDate = DateTime.Parse(competitionView.StartingDate);
                competition.Type = (int) competitionView.Type;
                competition.EndingDate = DateTime.Parse(competitionView.EndingDate);
                competition.City = (int) competitionView.City;
                competition.Guid = Guid.NewGuid();
                db.Competitions.Add(competition);
                db.SaveChanges();
            }
        }

        public List<CompetitionView> ReturnStartingCompetitions()
        {
            using (db = new CodersEntities())
            {
                List<Competition> competitions = (from u in db.Competitions where u.StartingDate > DateTime.Now select u).ToList();
                List<CompetitionView> competitionsView = new List<CompetitionView>();
                foreach (Competition c in competitions)
                {
                    competitionsView.Add(new CompetitionView(c));
                }
                return competitionsView;
            }
        }

        public List<GroupView> ReturnGroups(Guid competitionGuid)
        {
            using (db = new CodersEntities())
            {
                List<GroupView> groupView = new List<GroupView>();
                int id = (from c in db.Competitions where c.Guid == competitionGuid select c.Id).FirstOrDefault();
                List<Group> groups = (from g in db.Groups select g).Include(x => x.Participants).ToList();
                foreach(Group g in groups)
                {
                    foreach(Participant p in g.Participants)
                    {
                        if(p.Competition_Id == id)
                        {
                            groupView.Add(new GroupView(g));
                        }
                    }
                }

                //foreach (Group g in groups)
                //{
                //    groupView.Add(new GroupView(g));
                //}
                return groupView;
            }
        }

        public void SaveGroup(Group group, Guid competitionGuid, Guid userGuid)
        {
            using (db = new CodersEntities())
            {
                group.Guid = Guid.NewGuid();
                Guid guid = group.Guid;
                db.Groups.Add(group);
                db.SaveChanges();
                var groupId = (from g in db.Groups where g.Guid == guid select g.Id).FirstOrDefault();
                Member member = new Member();
                Participant participant = new Participant();
                member.Groups_Id = groupId;
                member.Users_Id = (from u in db.Users where u.Guid == userGuid select u.Id).FirstOrDefault();
                participant.Competition_Id = (from c in db.Competitions where c.Guid == competitionGuid select c.Id).FirstOrDefault();
                participant.Groups_Id = groupId;
                db.Members.Add(member);
                db.Participants.Add(participant);
                db.SaveChanges();
            }
        }

        public void JoinGroup(Guid groupGuid, Guid userGuid)
        {
            using (db = new CodersEntities())
            {
                Member member = new Member();
                Group group = (from g in db.Groups where g.Guid == groupGuid select g).FirstOrDefault();
                member.Groups_Id = group.Id;
                group.CountOfFounds = group.CountOfFounds + 1;
                member.Users_Id = (from u in db.Users where u.Guid == userGuid select u.Id).FirstOrDefault();
                db.Members.Add(member);
                db.SaveChanges();
            }
        }
    }
}