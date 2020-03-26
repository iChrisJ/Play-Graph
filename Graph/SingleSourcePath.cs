namespace Graph
{
    using System.Collections.Generic;
    using System;

    public class SingleSourcePath
    {
        private Graph G;
        private int s;
        private bool[] visited;
        private int[] prev;

        public SingleSourcePath(Graph G, int s)
        {
            this.G = G;
            this.s = s;
            visited = new bool[G.V];
            prev = new int[G.V];

            DFS(s, s);
        }

        private void DFS(int v, int parent)
        {
            visited[v] = true;
            prev[v] = parent;
            foreach (int w in G.Adj(v))
                if (!visited[w])
                    DFS(w, v);
        }

        public bool IsConnectedTo(int t)
        {
            G.ValidateVertex(t);
            return visited[t];
        }

        public IEnumerable<int> Path(int t)
        {
            List<int> res = new List<int>();
            if(!IsConnectedTo(t))
                return res;

            int cur = t;
            while(cur != s)
            {
                res.Add(cur);
                cur = prev[cur];
            }
            res.Add(s);

            res.Reverse();
            return res;
        }

        public static void Main()
        {
            Graph g = new Graph("g2.txt");
            SingleSourcePath sspath = new SingleSourcePath(g, 0);
            Console.WriteLine($"0 -> 6: {sspath.Path(6)}");
            Console.WriteLine($"0 -> 5: {sspath.Path(5)}");
        }
    }
}