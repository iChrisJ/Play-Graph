namespace Graph
{
    using System.Collections.Generic;
    using System;

    public class CycleDetection
    {
        private Graph G;
        private bool[] visited;

        private bool _hasCycle;
        public bool HasCycle
        {
            get { return _hasCycle; }
        }

        public CycleDetection(Graph G)
        {
            this.G = G;
            visited = new bool[G.V];

            for (int i = 0; i < G.V; i++)
                if (!visited[i])
                    if (DFS(i, i))
                    {
                        _hasCycle = true;
                        break;
                    }
        }

        private bool DFS(int v, int parent)
        {
            visited[v] = true;
            foreach (int w in G.Adj(v))
                if (!visited[w])
                {
                    if (DFS(w, v))
                        return true;
                }
                else if (w != parent)
                    return true;
            return false;
        }

        // public static void Main()
        // {
        //     Graph g = new Graph("g2.txt");
        //     CycleDetection cd = new CycleDetection(g);
        //     Console.WriteLine(cd.HasCycle);

        //     Graph g2 = new Graph("g3.txt");
        //     CycleDetection cd2 = new CycleDetection(g2);
        //     Console.WriteLine(cd2.HasCycle);
        // }
    }
}