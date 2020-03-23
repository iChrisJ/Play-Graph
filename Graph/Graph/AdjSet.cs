using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graph.Graph
{
    public class AdjSet
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

        private SortedSet<int>[] adj;

        public AdjSet(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                try
                {
                    string line = sr.ReadLine();
                    string[] nums = line.Split();
                    V = int.Parse(nums[0]);
                    adj = new SortedSet<int>[V];
                    for (int i = 0; i < V; i++)
                        adj[i] = new SortedSet<int>();
                    E = int.Parse(nums[1]);

                    for (int i = 0; i < E; i++)
                    {
                        line = sr.ReadLine();
                        string[] nodes = line.Split();
                        int a = GetVetex(nodes[0]);
                        int b = GetVetex(nodes[1]);

                        if (a == b)
                            throw new InvalidOperationException("Self loop is detected.");
                        if (adj[a].Contains(b))
                            throw new InvalidOperationException("Parallel edged are detected.");
                        adj[a].Add(b);
                        adj[b].Add(a);
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
            return adj[v].Contains(w);
        }

        public IEnumerable<int> Adj(int v)
        {
            ValidateVertex(v);
            return adj[v];
        }

        public int Degree(int v)
        {
            ValidateVertex(v);
            return adj[v].Count;
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            res.Append($"V = {V}, E = {E}\n");
            for (int i = 0; i < V; i++)
            {
                res.Append($"{i}: ");
                foreach (int w in adj[i])
                    res.Append($"{w} ");
                res.Append("\n");
            }

            return res.ToString();
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
        //     AdjSet adjSet = new AdjSet("g.txt");
        //     Console.Write(adjSet);
        // }
    }
}
