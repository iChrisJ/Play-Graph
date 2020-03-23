using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Graph.Graph
{
    public class Graph
    {
        private int _v;
        public int V
        {
            get { return _v; }
        }

        private int _e;
        public int E
        {
            get { return _e; }
        }

        private SortedSet<int>[] adj;

        public Graph(string filename)
        {
            using (StreamReader sr = File.OpenText(filename))
            {
                try
                {
                    string line = sr.ReadLine();
                    string[] nums = line.Split();
                    _v = int.Parse(nums[0]);
                    adj = new SortedSet<int>[_v];
                    for (int i = 0; i < _v; i++)
                        adj[i] = new SortedSet<int>();
                    _e = int.Parse(nums[1]);

                    for (int i = 0; i < _e; i++)
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
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
            res.Append($"V = {_v}, E = {_e}\n");
            for (int i = 0; i < _v; i++)
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
            if (v < 0 || v >= _v)
                throw new InvalidOperationException($"Vertex {v} is invalid.");
        }

        // public static void Main(string[] args)
        // {
            
        //     Graph graph = new Graph("g.txt");
        //     Console.Write(graph);
        // }
    }
}
