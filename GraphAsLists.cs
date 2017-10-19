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
        public void MakeAGraph(int City, int GroupId) //u bazi je nekim cudom grad int pa me mrzi sad da promenim
        {
            List<int> AllNodes;
            List<Solution> AllEdges;
            NodeNum = 0;
            Start = null;
            using (var db = new CodersEntities())
            {
                AllNodes = (from n in db.Landmarks where n.City == City select n.Id).ToList();
                AllEdges = (from e in db.Solutions where e.Groups_Id == GroupId select e).ToList();
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
                    if (ParentNode != null && ChildNode != null) break;

                    if (Pom.Id == Edg.LandmarkParent_Id) { ParentNode = Pom; }
                    if (Pom.Id == Edg.LandmarkChild_Id) { ChildNode = Pom; }

                    Pom = Pom.Next;
                }
                if (Pom == null) continue;

                ParentNode.Adj = new Edge { Dest = ChildNode, Link = ParentNode.Adj };

            }
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
            Queue<LinkedNode> que = new Queue<LinkedNode>(NodeNum);
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
                LinkedNode ParentNode = ptr;
                Edge adj = ptr.Adj;
                while (adj != null)
                {
                    LinkedNode ChildNode = adj.Dest;
                    Edge ChildAdj = ChildNode.Adj;
                    while (ChildAdj != null)
                    {
                        if (ChildAdj.Dest == ParentNode)
                        {
                            Bads.Add(new BadEdge { ParentNode = ParentNode.Id, ChildNode = ChildNode.Id });
                            break;
                        }
                        ChildAdj = ChildAdj.Link;
                    }
                    adj = adj.Link;
                }
                ptr = ptr.Next;
            }
            return Bads;
        }



        public void GenareRouts(int NumOfTeamMates)
        {
            LinkedNode min = Start;
            LinkedNode temp = Start.Next;
            LinkedNode end = null;
            if (Start.Adj == null)
                end = Start;
            while (temp != null)
            {
                if (temp.Id < min.Id)
                    min = temp;
                if (end != null && temp.Adj == null)
                    end = temp;
                temp.TeamMate = 0;
                temp = temp.Next;
            }
            SetLevels(1, min);
            SetRouts(NumOfTeamMates, min, end);
            List<int> numOfNodes = new List<int>(3);
            for (int i = 0; i < NumOfTeamMates; i++)
                numOfNodes[i] = NumOfNodes(i - 1, min, end);
            LinkedNode t = Start;
            while(t!=null)
            {
                if(t.path.Count==0)
                {
                    int p1 = numOfNodes.Min();

                }
            }

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
                positionn[i] = start;
            int endOfRoad = 0;
            int Player = 1;
            start.TeamMate = 1;
            while (endOfRoad < num)
            {
                int x = NextNode(Player, positionn[Player - 1], end, positionn);
                if (x == 0)
                    endOfRoad++;
                Player++;
                if (Player == num)
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
                int x = NextNode(p, temp.Dest, end, positionn);
                if (x == 2)
                {
                    pos.path.Add(p);
                }
                if (x != 1)
                {
                    return x;
                }
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
                if (temp.Dest.path.Contains(p))
                    return 1 + NumOfNodes(p, temp.Dest, end);


            }
            return 0;

        }
    }
       
}
