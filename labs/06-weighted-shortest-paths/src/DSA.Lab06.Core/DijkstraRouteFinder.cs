using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Lab06
{
    public sealed class DijkstraRouteFinder
    {
        public RouteResult FindRoute(WeightedGraph graph, string start, string goal)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsNode(start) || !graph.ContainsNode(goal))
            {
                return RouteResult.NotFound(0);
            }

            if (start == goal)
            {
                return RouteResult.Success(new[] { start }, 0, 0);
            }

            var bestCost = new Dictionary<string, int>();
            var cameFrom = new Dictionary<string, string>();
            var frontier = new MinFrontier<string>();

            bestCost[start] = 0;
            frontier.Enqueue(start, 0);

            int expandedNodes = 0;

            while (frontier.Count > 0)
            {
                // Originally did just the string, understood the entry is needed for finding a stale entry
                MinFrontier<string>.Entry entry = frontier.Dequeue();
                string cur = entry.Value;

                if (!bestCost.TryGetValue(cur, out int currentBest) || entry.Priority != currentBest)
                {
                    continue;
                }

                expandedNodes++;

                if (cur == goal)
                {
                    Stack<string> path = new Stack<string>();
                    string backlog = cur;

                    while (backlog != start)
                    {
                        path.Push(backlog);
                        backlog = cameFrom[backlog];
                    }
                    path.Push(backlog);
                    List<string> act = path.ToList();
                    return RouteResult.Success(act, bestCost[cur], expandedNodes);
                }

                foreach (WeightedEdge edge in graph.GetOutgoing(cur))
                {
                    int candidate = checked(currentBest + edge.Cost);

                    if(!bestCost.TryGetValue(edge.To, out int known) || candidate < known)
                    {
                        bestCost[edge.To] = candidate;
                        cameFrom[edge.To] = cur;
                        frontier.Enqueue(edge.To, candidate);
                    }
                }
            }

            // TODO Lab 6A:
            // 1. Repeatedly remove the smallest-priority frontier candidate.
            // 2. Ignore stale entries when a cheaper cost is already known.
            // 3. Finalize/expand the current node.
            // 4. If it is the goal, reconstruct the route using cameFrom.
            // 5. Relax each outgoing edge:
            //      candidate = bestCost[current] + edge.Cost
            //    If candidate improves the known cost, update both bestCost and cameFrom,
            //    then enqueue the neighbor with the improved total cost.
            //
            // Implement one small piece at a time and run the relevant tests.

            return RouteResult.NotFound(expandedNodes);
        }
    }
}
