using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graph.Graph
{
    public class AdjMatrix
    {
        private int V;
        public int Vertex
        {
            get { return V; }
        }

        private int E;
        public int Edge
        {
            get { return E; }
        }

        private int[,] adj;

        public AdjMatrix(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                try
                {
                    string line = sr.ReadLine();
                    string[] nums = line.Split();
                    V = int.Parse(nums[0]);
                    adj = new int[V, V];
                    E = int.Parse(nums[1]);

                    for (int i = 0; i < E; i++)
                    {
                        line = sr.ReadLine();
                        string[] nodes = line.Split();
                        int a = GetVetex(nodes[0]);
                        int b = GetVetex(nodes[1]);

                        if (a == b)
                            throw new InvalidOperationException("Self loop is detected.");
                        if (adj[a, b] == 1)
                            throw new InvalidOperationException("Parallel edged are detected.");
                        adj[a, b] = 1;
                        adj[b, a] = 1;
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }

        public bool HasEdge(int v, int w)
        {
            ValidateVertex(v);
            ValidateVertex(w);
            return adj[v, w] == 1;
        }

        public IList<int> Adj(int v)
        {
            ValidateVertex(v);
            IList<int> res = new List<int>();
            for (int i = 0; i < V; i++)
                if(adj[v, i] == 1)
                    res.Add(i);
            return res;
        }

        public int Degree(int v)
        {
            return Adj(v).Count;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.Append($"V = {V}, E = {E}\n");
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                    str.Append($"{adj[i, j]} ");
                str.Append("\n");
            }

            return str.ToString();
        }

        private int GetVetex(string node)
        {
            if (string.IsNullOrEmpty(node))
                throw new InvalidOperationException($"Vertex {node} is invalid.");

            int v = int.Parse(node);
            ValidateVertex(v);
            return v;
        }

        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new InvalidOperationException($"Vertex {v} is invalid.");
        }
        // public static void Main(string[] args)
        // {
        //     AdjMatrix adjMatrix = new AdjMatrix("g.txt");
        //     Console.Write(adjMatrix);
        // }
    }
}
