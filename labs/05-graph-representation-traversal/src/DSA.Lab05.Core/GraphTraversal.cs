using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;

namespace DSA.Lab05
{
    /// <summary>
    /// Iterative graph traversals.
    ///
    /// Complete these methods after the adjacency representation is working.
    /// Think in terms of:
    /// frontier + visited + neighbors.
    /// </summary>
    public static class GraphTraversal
    {
        public static IReadOnlyList<string> BreadthFirstOrder(AdjacencyGraph graph, string start)
        {
            List<string> result = new List<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                IReadOnlyList<string> froms = graph.GetNeighbors(queue.Peek());
                foreach (string s in froms)
                {
                    if (!result.Contains(s) && !queue.Contains(s))
                    {
                        queue.Enqueue(s);
                    }
                }
                result.Add(queue.Dequeue());
            }

            return result;
        }

        public static IReadOnlyList<string> DepthFirstOrder(AdjacencyGraph graph, string start)
        {
            List<string> result = new List<string>();
            Stack<string> stack = new Stack<string>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                string current = stack.Pop();
                result.Add(current);

                IReadOnlyList<string> strings = graph.GetNeighbors(current);

                for (int i = strings.Count-1; i >= 0; i--)
                {
                    if (!result.Contains(strings[i]) && !stack.Contains(strings[i]))
                    {
                        stack.Push(strings[i]);
                    }
                }
            }
            return result;
        }

        public static bool IsReachableBfs(AdjacencyGraph graph, string start, string target)
        {
            if (graph == null)
            {
                throw new ArgumentNullException();
            }

            Queue<string> queue = new Queue<string>();
            HashSet<string> visited = new HashSet<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                if (current == target)
                {
                    return true;
                }
                foreach (string s in graph.GetNeighbors(current))
                {
                    if (visited.Add(s))
                    {
                        queue.Enqueue(s);
                    }
                }
            }

            return false;
        }
    }
}
