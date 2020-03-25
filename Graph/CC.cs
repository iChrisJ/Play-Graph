using System;

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

        public static void Main()
        {
            Graph g = new Graph("g2.txt");
            CC cc = new CC(g);
            Console.WriteLine(cc.CCCount);
        }
    }
}