using System.Collections.Generic;
using System;

namespace Graph
{
    public class GraphBFS
    {
        private Graph G;
        private bool[] visited;
        private IList<int> _order;
        public IList<int> Order
        {
            get { return _order; }
        }

        public GraphBFS(Graph G)
        {
            this.G = G;
            visited = new bool[G.V];
            _order = new List<int>();
            for (int i = 0; i < G.V; i++)
                if (!visited[i])
                    BFS(i);
        }

        private void BFS(int v)
        {
            Queue<int> queue = new Queue<int>();
            visited[v] = true;
            queue.Enqueue(v);

            while(queue.Count > 0)
            {
                int frnt = queue.Dequeue();
                _order.Add(frnt);

                foreach (int w in G.Adj(frnt))
                    if (!visited[w])
                    {
                        visited[w] = true;
                        queue.Enqueue(w);
                    }
            }
        }

        public static void Main()
        {
            Graph g = new Graph("g3.txt");
            GraphBFS bfs = new GraphBFS(g);
            Console.WriteLine(bfs.Order);
        }
    }
}