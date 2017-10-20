using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coders.Graph
{
    public class GraphAsLists
    {
        public LinkedNode Start { get; set; }
       public int NodeNum { get; set; }
       public void MakeAGraph( int City,int CompetetionId) //u bazi je nekim cudom grad int pa me mrzi sad da promenim
       {
            List<int> AllNodes;
            List<Solution> AllEdges;
            NodeNum = 0;
            Start = null;
            using(var db =new CodersEntities())
            {
                AllNodes = (from n in db.Landmarks /*where n.City==City*/ select n.Id).ToList();
                AllEdges = (from e in db.Solutions /*where e.Competition_Id==CompetetionId*/ select e).ToList();
            }
            NodeNum = AllNodes.Count;

             foreach(var Node in AllNodes)
              {
               Start= new LinkedNode { Id = Node, Adj = null, Next = Start, Status = 0 };

                }

              foreach(var Edg in AllEdges)
               {
                LinkedNode ParentNode = null,ChildNode = null,Pom = Start;
                while (Pom != null )
                {
                    
                   
                    if (Pom.Id == Edg.LandmarkParent_Id){ ParentNode = Pom; }
                    if (Pom.Id == Edg.LandmarkChild_Id) {  ChildNode = Pom; }

                    if (ParentNode != null && ChildNode != null) break;
                    Pom = Pom.Next;
                }
                if (Pom==null) continue;
   
                ParentNode.Adj= new Edge { Dest = ChildNode, Link =ParentNode.Adj };

                }
            }

        public void MakeAGraph(int City, int CompetetionId, List<Solution> AllEdges) //u bazi je nekim cudom grad int pa me mrzi sad da promenim
        {
            List<int> AllNodes;
            NodeNum = 0;
            Start = null;
            using (var db = new CodersEntities())
            {
                AllNodes = (from n in db.Landmarks /*where n.City==City*/ select n.Id).ToList();
            }
                NodeNum = AllNodes.Count;


            foreach (var Node in AllNodes)
            {
                Start = new LinkedNode { Id = Node, Adj = null, Next = Start, Status = 0 };

            }

            foreach (var Edg in AllEdges)
            {
                LinkedNode ParentNode = null, ChildNode = null, Pom = Start;
                while (Pom != null)
                {
                    

                    if (Pom.Id == Edg.LandmarkParent_Id) { ParentNode = Pom; }
                    if (Pom.Id == Edg.LandmarkChild_Id) { ChildNode = Pom; }

                    if (ParentNode != null && ChildNode != null) break;

                    Pom = Pom.Next;
                }
                if (Pom == null) continue;

                ParentNode.Adj = new Edge { Dest = ChildNode, Link = ParentNode.Adj };

            }
        }
        public List<LinkedNode> AllPathInRim()
        {

            LinkedNode ptr;
            ptr = Start;
            while (ptr != null)
            {
                ptr.Status = 0;
                ptr.inQueue = false;
                ptr = ptr.Next;
            }
            ptr = Start;
            while (ptr != null)
            {
                Edge pot = ptr.Adj;
                if (pot == null)
                {
                    ptr.inQueue = true;
                }
                while (pot != null)
                {
                    pot.Dest.Status += 1;
                    pot = pot.Link;
                }
                ptr = ptr.Next;
            }
            List<LinkedNode> EndPoints = new List<LinkedNode>();
            ptr = Start;
            while (ptr != null)
            {
                if(ptr.inQueue==true && ptr.Status > 0)
                {
                    EndPoints.Add(ptr);
                }
                ptr = ptr.Next;
            }
            return EndPoints;

        }

        public List<BadEdge> ConnectEdge(List<LinkedNode> EndPoints)
        {
            LinkedNode End =EndPoints.FirstOrDefault();
            if (End == null)
            {
                return null;
            }
            for(var i=0;i< EndPoints.Count;i++)
            {
                if (End.Status <= EndPoints[i].Status)
                {
                    End = EndPoints[i];
                }
            }
            List<BadEdge> list = new List<BadEdge>();
            
                for (var i= 0; i < EndPoints.Count; i++)
                {
                    if (EndPoints[i] == End)
                    {
                        continue;
                    }
                    
                    list.Add(new BadEdge { ParentNode = EndPoints[i].Id, ChildNode = End.Id });
                  
                }
             
            
            return list;

            

        }

        public List<BadEdge> topologicalOrderTrav()
        {
            int retVal = 0;

            LinkedNode ptr;
            ptr = Start;
            while (ptr != null)
            {
                ptr.Status = 0;
                ptr.inQueue = false;
                ptr = ptr.Next;
            }
            ptr = Start;
            while (ptr != null)
            {
                Edge pot = ptr.Adj;
                while (pot != null)
                {
                    pot.Dest.Status += 1;
                    pot = pot.Link;
                }
                ptr = ptr.Next;
            }
            Queue<LinkedNode> que= new Queue<LinkedNode>(NodeNum);
            ptr = Start;
            while (ptr != null)
            {
                if (ptr.Status == 0)
                {
                    que.Enqueue(ptr);
                    ptr.inQueue = true;
                }
                ptr = ptr.Next;

            }
            while (que.Count > 0)
            {
                ptr = que.Dequeue();
                retVal += 1;
                Edge pot = ptr.Adj;
                while (pot != null)
                {
                    pot.Dest.Status -= 1;
                    if (pot.Dest.Status == 0)
                    {
                        que.Enqueue(pot.Dest);
                        pot.Dest.inQueue = true;
                    }
                    pot = pot.Link;
                }

            }
            if (retVal == NodeNum)
            {
                return null;
            }
            List<BadEdge> Bads = new List<BadEdge>();
            ptr = Start;
            while (ptr != null)
            {
                if (ptr.inQueue)
                {
                    ptr = ptr.Next;
                    continue;
                }
                Edge adj = ptr.Adj;
                while (adj != null)
                {

                    Bads.Add(new BadEdge { ParentNode =ptr.Id, ChildNode = adj.Dest.Id });
                    adj = adj.Link;
                }
                     
                ptr = ptr.Next;
            }
            return Bads;
        }
		public LinkedNode getStart()
		{
			LinkedNode st=Start;
			while(st!=null)
			{
				st.Status=0;
				st=st.Next;
			}
			st=Start;
			while(st!=null)
			{
				Edge t=st.Adj;
				while(t!=null)
				{
					t.Dest.Status++;
					t=t.Link;
				}
				st=st.Next;
			}
			st=Start;
			while(st!=null)
			{
				if(st.Status==0)
					return st;
				st=st.Next;
			}
			return null;
		}
        public List<Routs> GenareRouts(int NumOfTeamMates)
        {
            LinkedNode min = getStart();
            LinkedNode temp = Start.Next;
            LinkedNode end = null;
            if (Start.Adj == null)
                end = Start;
            while (temp != null)
            {
                if (end == null && temp.Adj == null)
                    end = temp;
                temp.TeamMate = 0;
                temp = temp.Next;
            }
            SetLevels(1, min);
            SetRouts(NumOfTeamMates, min, end);
            List<int> numOfNodes = new List<int>(3);
            for (int i = 0; i < NumOfTeamMates; i++)
                numOfNodes.Add(NumOfNodes(i + 1, min, end));
            LinkedNode t = Start;
            while (t != null)
            {
                Edge s = t.Adj;
                while(s!=null)
                {
                    if (s.path.Count == 0 && s.Dest.TeamMate==0)
                    {
                        int p1 = numOfNodes.Min();
                        LinkedNode pr = GetSecondToLastNode(numOfNodes.FindIndex(x => x == p1) + 1, min, end);
                        Edge g = pr.Adj;
                        while(g!=null)
                        {
                            if (g.Dest == end)
                                g.path.Remove(numOfNodes.FindIndex(x => x == p1) + 1);
                            g = g.Link;
                        }
                        addEdge(pr, t).path.Add(numOfNodes.FindIndex(x => x == p1)+1);
                        addEdge(t, end).path.Add(numOfNodes.FindIndex(x => x == p1) + 1);
                        t.TeamMate = p1;
                       // s.path.Add(p1);
                        numOfNodes[numOfNodes.FindIndex(x => x == p1)]++;
                    }
                    s = s.Link;
                }
                t = t.Next;
            }
            return CreateRouts();
        }

        public List<Routs> CreateRouts()
        {
            List<Routs> temp = new List<Routs>();
            LinkedNode s = Start;
            while(s!=null)
            {
                Edge t = s.Adj;
                while (t != null)
                {
                    temp.Add(new Routs { ParentNode = s.Id, ChildNode = t.Dest.Id, path = t.path });
                    t = t.Link;
                }
                s = s.Next;
            }
            return temp;

        }
        public Edge addEdge(LinkedNode a, LinkedNode b)
        {
            Edge tmp = a.Adj;
            while(tmp!=null)
            {
                if (tmp.Dest == b)
                    return tmp;
                tmp = tmp.Link;
            }

            return a.Adj = new Edge { Dest = b, Link = a.Adj };
        }
        public void RemoveEdge(LinkedNode a, LinkedNode b)
        {
            Edge temp = a.Adj;
            if (temp.Dest == b)
            {
                a.Adj = temp.Link;
                return;
            }
            while (temp.Link != null && temp.Link.Dest != b) { temp = temp.Link; }
            if (temp.Link == null) return;
            temp.Link = temp.Link.Link;
        }

        public LinkedNode GetSecondToLastNode(int p, LinkedNode pos, LinkedNode end)
        {
            if (pos == end)
                return null;
            Edge temp = pos.Adj;
            while (temp != null)
            {
                if (temp.path.Contains(p))
                {
                    LinkedNode s = GetSecondToLastNode(p, temp.Dest, end);
                    if (s == null)
                        return pos;
                    if (s != null)
                        return s;
                }
                temp = temp.Link;
            }
            return null;
        }


        public void SetLevels(int index, LinkedNode node)
        {
            node.Level = index;
            Edge temp = node.Adj;
            while (temp != null)
            {
                SetLevels(index++, temp.Dest);
                temp = temp.Link;
            }
        }
        public void SetRouts(int num, LinkedNode start, LinkedNode end)
        {
            List<LinkedNode> positionn = new List<LinkedNode>(num);
            for (int i = 0; i < num; i++)
                positionn.Add(start);
            int endOfRoad = 0;
            int Player = 1;
            start.TeamMate = 1;
            while (endOfRoad < num)
            {
				int x=0;
                if(positionn[Player-1]!=end)
				{
					x = NextNode(Player, positionn[Player - 1], end, positionn);
				}
                if (x == 0)
                    endOfRoad++;
                Player++;
                if (Player > num)
                    Player = 1;
            }


        }

        public int NextNode(int p, LinkedNode pos, LinkedNode end, List<LinkedNode> positionn)
        {
            if (pos == end)
                return 0;
            if (pos.TeamMate == 0)
            {
                pos.TeamMate = p;
                pos.path.Add(p);
                positionn[p - 1] = pos;
                return 2;
            }
            Edge temp = pos.Adj;
            while (temp != null)
            {
                if (temp.Dest.TeamMate == 0)
                {
                    pos.path.Add(p);
                    temp.Dest.TeamMate = p;
                    temp.path.Add(p);
                    positionn[p - 1] = temp.Dest;
                    return 2;
                }
                temp = temp.Link;
            }
			pos.Adj.path.Add(p);
            int x = NextNode(p, pos.Adj.Dest, end, positionn);
            if (x == 2)
            {
                //pos.path.Add(p);
                return 2;
            }
            if (x != 1)
            {
                return x;
            }
            return 1;
        }
        public int NumOfNodes(int p, LinkedNode pos, LinkedNode end)
        {
            if (pos == end)
                return 1;
            Edge temp = pos.Adj;
            while (temp != null)
            {
                if (temp.path.Contains(p))
                    return 1 + NumOfNodes(p, temp.Dest, end);

                temp = temp.Link;
            }
            return 0;
        }
    }

}
