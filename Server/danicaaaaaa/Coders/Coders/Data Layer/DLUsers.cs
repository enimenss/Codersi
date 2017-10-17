using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coders.Models;

namespace Coders.Data_Layer
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
            return (from u in db.Users select u).Count();
        }

        public List<UserView> ReturnUsers(int skip, int take)
        {
            List<User> users = (from l in db.Users select l).Skip(skip).Take(take).ToList();
            List<UserView> usersView = new List<UserView> ();
            foreach (User u in users)
            {
                usersView.Add(new  UserView(u));
            }
            return usersView;
        }
    }
}