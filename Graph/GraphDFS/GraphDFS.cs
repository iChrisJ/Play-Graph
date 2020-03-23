namespace Graph.GraphDFS
{
    using System.Collections.Generic;
    using System;
    using Graph;

    public class GraphDFS
    {
        private Graph G;
        private bool[] visited;
        private IList<int> _preOrder;
        public IList<int> PreOrder
        {
            get { return _preOrder; }
        }

        private IList<int> _postOrder;
        public IList<int> PostOrder
        {
            get { return _postOrder; }
        }
        public GraphDFS(Graph G)
        {
            this.G = G;
            visited = new bool[G.V];
            _preOrder = new List<int>();
            _postOrder = new List<int>();

            for (int i = 0; i < G.V; i++)
                if (!visited[i])
                    DFS(0);
        }

        private void DFS(int v)
        {
            visited[v] = true;
            _preOrder.Add(v);
            foreach (int w in G.Adj(v))
                if (!visited[w])
                    DFS(w);
            _postOrder.Add(v);
        }

        public static void Main()
        {
            Graph g = new Graph("g2.txt");
            GraphDFS dfs = new GraphDFS(g);
            Console.Write(dfs.PreOrder);
            Console.Write(dfs.PostOrder);
        }
    }
}