using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace Coders
{
    [HubName("CodersHub")]
    public class CodersHub : Hub
    {
        private readonly static ConnectionMapping<string> Connections = ConnectionMapping<string>.GetConection;

        private CodersEntities db = new CodersEntities();

        public void javiSe(string guid)
        {

            foreach (var connectionId in Connections.GetConnections(guid))
            {
                Clients.Client(connectionId).jsJaviSe();

            }

        }


        public void changeLocation(float x,float y)
        {
            string name = Context.QueryString["GUID"].ToString();
            int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            List<string> MembersGuid = (from m in db.Members where m.Groups_Id == Group select m.User.Guid.ToString()).ToList();
            foreach(string UserGuid in MembersGuid)
            {
                foreach (var connectionId in Connections.GetConnections(UserGuid))
                {
                    Clients.Client(connectionId).updateClientPosition(x,y,name);

                }
            }

        }


        public void posaljiPoruku(string mesage)
        {
            string name = Context.QueryString["GUID"].ToString();
            User user = (from s in db.Users where s.Guid.ToString() == name select s).SingleOrDefault();
            string slika;
            slika = "cover.jpg";

            int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            List<string> MembersGuid = (from m in db.Members where m.Groups_Id == Group select m.User.Guid.ToString()).ToList();
            foreach (string UserGuid in MembersGuid)
            {
                foreach (var connectionId in Connections.GetConnections(UserGuid))
                {
                    Clients.Client(connectionId).primiPoruku(mesage, name, slika, DateTime.Now.ToString(), user.Named);

                }
            }

           

            Message poruka = new Message { Users_Id = user.Id, Date = DateTime.Now, Message1 = mesage,Groups_Id=user.Members.FirstOrDefault().Groups_Id };
        
  
                db.Messages.Add(poruka);
                db.SaveChanges();
 
        }

        public void typping(string target)
        {
            string name = Context.QueryString["GUID"].ToString();

            User user = (from s in db.Users where s.Guid.ToString() == name select s).SingleOrDefault();
            string slika;
            slika = "cover.jpg";
            int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
            List<string> MembersGuid = (from m in db.Members where m.Groups_Id == Group select m.User.Guid.ToString()).ToList();
            foreach (string UserGuid in MembersGuid)
            {
                foreach (var connectionId in Connections.GetConnections(UserGuid))
                {
                    Clients.Client(connectionId).typping(name, slika);

                }
            }

        }

        public void untypping(string target)
        {
            {
                string name = Context.QueryString["GUID"].ToString();

                int? Group = (from u in db.Users where u.Guid.ToString() == name select u.Members.FirstOrDefault().Groups_Id).FirstOrDefault();
                List<string> MembersGuid = (from m in db.Members where m.Groups_Id == Group select m.User.Guid.ToString()).ToList();
                foreach (string UserGuid in MembersGuid)
                {
                    foreach (var connectionId in Connections.GetConnections(UserGuid))
                    {
                        Clients.Client(connectionId).untypping(name);

                    }
                }
   
            }
        }

        public override Task OnConnected()
        {
            string name = Context.QueryString["GUID"].ToString();



            Connections.Add(name, Context.ConnectionId);


            return base.OnConnected();
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.QueryString["GUID"].ToString();



            Connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.QueryString["GUID"].ToString();

           
             Connections.Add(name, Context.ConnectionId);
            
            return base.OnReconnected();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}