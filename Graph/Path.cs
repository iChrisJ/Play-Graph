namespace Graph
{
    using System.Collections.Generic;
    using System;

    public class Path
    {
        private Graph G;
        private int s;
        private int t;
        private bool[] visited;
        private int[] prev;

        public Path(Graph G, int s, int t)
        {
            this.G = G;
            this.s = s;
            this.t = t;
            visited = new bool[G.V];
            prev = new int[G.V];
            Array.Fill<int>(prev, -1);

            DFS(s, s);
        }

        private bool DFS(int v, int parent)
        {
            visited[v] = true;
            prev[v] = parent;
            if (v == t)
                return true;
            foreach (int w in G.Adj(v))
                if (!visited[w])
                    if (DFS(w, v))
                        return true;

            return false;
        }

        public bool IsConnected()
        {
            return visited[t];
        }

        public IEnumerable<int> PathList()
        {
            List<int> res = new List<int>();
            if (!IsConnected())
                return res;

            int cur = t;
            while (cur != s)
            {
                res.Add(cur);
                cur = prev[cur];
            }
            res.Add(s);

            res.Reverse();
            return res;
        }

        // public static void Main()
        // {
        //     Graph g = new Graph("g2.txt");
        //     Path path = new Path(g, 0, 6);
        //     Console.WriteLine($"0 -> 6: {path.PathList()}");

        //     Path path2 = new Path(g, 0, 5);
        //     Console.WriteLine($"0 -> 5: {path2.PathList()}");

        //     Path path3 = new Path(g, 0, 1);
        //     Console.WriteLine($"0 -> 1: {path3.PathList()}");
        // }
    }
}