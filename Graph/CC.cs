using System;
using System.Collections.Generic;

namespace Graph
{
    public class CC
    {
        private Graph G;
        private int[] visited;

        private int _cccount;
        public int CCCount
        {
            get { return _cccount; }
        }

        public CC(Graph G)
        {
            this.G = G;
            visited = new int[G.V];
            Array.Fill<int>(visited, -1);

            for (int i = 0; i < G.V; i++)
                if (visited[i] == -1)
                {
                    DFS(i, _cccount);
                    _cccount++;
                }
        }

        private void DFS(int v, int ccid)
        {
            visited[v] = ccid;

            foreach (int w in G.Adj(v))
                if (visited[w] == -1)
                    DFS(w, ccid);
        }

        public bool IsConnected(int v, int w)
        {
            G.ValidateVertex(v);
            G.ValidateVertex(w);
            return visited[v] == visited[w];
        }

        public IList<int>[] Components()
        {
            IList<int>[] res = new IList<int>[_cccount];
            for (int i = 0; i < CCCount; i++)
                res[i] = new List<int>();
            
            for(int v = 0; v < G.V; v++)
                res[visited[v]].Add(v);
            return res;
        }

        public static void Main()
        {
            Graph g = new Graph("g2.txt");
            CC cc = new CC(g);
            Console.WriteLine(cc.CCCount);

            Console.WriteLine(cc.IsConnected(0, 6));
            Console.WriteLine(cc.IsConnected(0, 5));

            IList<int>[] comp = cc.Components();
            for (int i = 0; i < comp.Length; i++)
            {
                Console.Write($"{i}: ");
                foreach (int w in comp[i])
                    Console.Write($"{w} ");
                Console.WriteLine();
            }
        }
    }
}