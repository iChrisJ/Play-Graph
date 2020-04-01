namespace Graph
{
    using System.Collections.Generic;
    using System;

    public class BipartitionDetection
    {
        private Graph G;
        private bool[] visited;
        private int[] colors;
        private bool isBipartite;
        public bool IsBipartite
        {
            get { return isBipartite; }
        }

        public BipartitionDetection(Graph G)
        {
            isBipartite = true;
            this.G = G;
            visited = new bool[G.V];
            colors = new int[G.V];
            Array.Fill<int>(colors, -1);

            for (int i = 0; i < G.V; i++)
                if (!visited[i])
                    if(!DFS(i, 0))
                    {
                        isBipartite = false;
                        break;
                    }
        }

        private bool DFS(int v, int color)
        {
            visited[v] = true;
            colors[v] = color;
            foreach (int w in G.Adj(v))
                if (!visited[w])
                {
                    if (!DFS(w, 1 - color))
                        return false;
                }
                else if(colors[v] == colors[w] )
                    return false;
            return true;
        }

        // public static void Main()
        // {
        //     Graph g = new Graph("g2.txt");
        //     BipartitionDetection bd = new BipartitionDetection(g);
        //     Console.WriteLine(bd.IsBipartite);

        //     Graph g2 = new Graph("g4.txt");
        //     BipartitionDetection bd2 = new BipartitionDetection(g2);
        //     Console.WriteLine(bd2.IsBipartite);

        //     Graph g3 = new Graph("g5.txt");
        //     BipartitionDetection bd3 = new BipartitionDetection(g3);
        //     Console.WriteLine(bd3.IsBipartite);
        // }
    }
}