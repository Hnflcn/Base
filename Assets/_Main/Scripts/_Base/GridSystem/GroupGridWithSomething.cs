using System;
using System.Collections.Generic;
using System.Linq;

namespace _Main.Scripts._Base.GridSystem
{
    public static class GroupGridWithSomething<T>
    {
        public static List<T> FindMatchTeams(T item, List<T> neighbours,  Func<T, T, bool> matchCondition)
        {
            var visited = new HashSet<T>();
            var queue = new Queue<T>();

            var matchedItems = new HashSet<T>();
            
            queue.Enqueue(item);
            visited.Add(item);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                matchedItems.Add(current);

                foreach (var neighbor in neighbours
                             .Where(neighbor => matchCondition(neighbor, item) && !visited.Contains(neighbor)))
                {
                    queue.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }

            return matchedItems.ToList();
        }
    }
}