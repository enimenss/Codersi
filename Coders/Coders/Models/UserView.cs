using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using CodersJoca;

namespace CodersJoca.Models
{
    public class UserView
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Named { get; set; }
        public string Surname { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        public System.Guid Guid { get; set; }

        public UserView() { }

        public UserView(User u)
        {
            Id = u.Id;
            Username = u.Username;
            Pass = u.Pass;
            Named = u.Named;
            Surname = u.Surname;
            Email = u.Email;
            Guid = u.Guid;
        }
    }
}