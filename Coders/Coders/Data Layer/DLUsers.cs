using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodersJoca.Models;

namespace CodersJoca.Data_Layer
{
    public class DLUser
    {
        private static DLUser data;
        CodersEntities db;

        private DLUser() { }

        public static DLUser Data
        {
            get
            {
                if (data == null)
                {
                    data = new DLUser();
                }
                return data;
            }
        }

        public List<UserView> ReturnUsers()
        {
            using (db = new CodersEntities())
            {
                List<User> users = (from u in db.Users select u).ToList();
                List<UserView> usersView = new List<UserView>();
                foreach (User u in users)
                {
                    usersView.Add(new UserView(u));
                }
                return usersView;
            }
        }

        public int ReturnUserCount()
        {
            using (db = new CodersEntities())
            {
                return (from u in db.Users select u).ToList().Count;
            }
        }

        public List<UserView> ReturnUsers(int skip, int take)
        {
            using (db = new CodersEntities())
            {
                List<User> users = (from l in db.Users select l).OrderBy(x => x.Id).Skip(skip).Take(take).ToList();
                List<UserView> usersView = new List<UserView>();
                foreach (User u in users)
                {
                    usersView.Add(new UserView(u));
                }
                return usersView;
            }
        }

        public void DeleteUser(int id)
        {
            using (db = new CodersEntities())
            {
                User user = db.Users.Find(id);
                db.Users.Attach(user);
                //user.Deleted = 1;
                db.SaveChanges();
            }
        }

        public int FindUsersId(Guid guid)
        {
            using (db = new CodersEntities())
            {
                return (from u in db.Users where u.Guid == guid select u.Id).FirstOrDefault();
            }
        }
    }
}