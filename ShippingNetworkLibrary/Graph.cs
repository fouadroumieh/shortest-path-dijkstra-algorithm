using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingNetworkLibrary
{
    public class Graph
    {
        List<List<Edge>> adjacencyList;  // adjacency list
        int[][] adjacencyMatrix; // adjacency matrix
        Dictionary<string, int> nodeName;  // name of the nodes (string -> int)
        List<string> name;                 // name of the nodes (int -> string)
        int V;  // number of vertices in the graph

        public Graph()
        {
            adjacencyList = new List<List<Edge>>();
            nodeName = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            adjacencyMatrix = null;
            name = new List<string>();
            V = 0;
        }

        // build the adjacency matrix from the adjacency list
        private void BuildMatrix()
        {
            adjacencyMatrix = new int[V][];
            for (int i = 0; i < V; i++)
                adjacencyMatrix[i] = new int[V];
            foreach (List<Edge> list in adjacencyList)
            {
                foreach (Edge e in list)
                {
                    adjacencyMatrix[e.u][e.v] = e.w;
                }
            }
        }

        // loads the graph from a file
        public static Graph LoadGraph(string fileName)
        {
            Graph g = new Graph();
            string line;

            try
            {
                System.IO.StreamReader file =
                   new System.IO.StreamReader(fileName);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('=');
                    string[] cities = parts[0].Split('>');
                    int days = Convert.ToInt32(parts[1].Trim().Split(' ')[0]);
                    g.NewEdge(cities[0].Trim(), cities[1].Trim(), days);
                }
                file.Close();
                g.BuildMatrix();
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File not found: " + fileName);
            }

            return g;
        }

        // loads the routes from a file
        public static List<string> LoadRoutes(string fileName)
        {

            List<string> routes = new List<string>();
            string line;

            try
            {
                using (System.IO.StreamReader file =
                   new System.IO.StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        routes.Add(line);
                    }
                };

            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File not found: " + fileName);
            }

            return routes;
        }
        // adds a new edge from v1 to v2 with weight w
        public void NewEdge(String v1, String v2, int w)
        {
            int n = V;  // number of vertices
            int u = newVertex(v1);
            int v = newVertex(v2);

            // add entries to the adjacency matrix if needed
            if (u >= n)
            {
                adjacencyList.Add(new List<Edge>());
                n++;
            }
            if (v >= n)
            {
                adjacencyList.Add(new List<Edge>());
                n++;
            }

            adjacencyList[u].Add(new Edge(u, v, w));
        }

        // adds a new vertex to the garph if needed, and returns its index
        int newVertex(String name)
        {
            if (nodeName.ContainsKey(name))
                return nodeName[name];

            nodeName[name] = V;
            this.name.Add(name);
            return V++;
        }

        // computes the journey time for the given path (or -1 if invalid path)
        public int GetJourneyTime(string[] path)
        {
            foreach (string c in path)
            {
                if (!nodeName.ContainsKey(c.Trim()))
                    return -2;
            }
            int time = 0;
            int u = nodeName[path[0]];
            for (int i = 0; i < path.Length - 1; i++)
            {
                int v = nodeName[path[i + 1]];
                if (adjacencyMatrix[u][v] == 0) // no edge from u to v
                    return -1;
                time += adjacencyMatrix[u][v];
                u = v;
            }
            return time;
        }

        // computes shortest time journey from the given city to the destination
        public List<string> GetShortestPath(string source, string destination)
        {
            List<string> path = new List<string>();

            if (!nodeName.ContainsKey(source) || !nodeName.ContainsKey(destination))
                return null;

            int u = nodeName[source];
            int v = nodeName[destination];
            int[] dist;  // dist[i] is the distance to node i from u
            int[] prev;  // prev[i] is the previous node in the path from u to v
            int[] visited; // has node i been visited yet?
            dist = new int[V];
            prev = new int[V];
            visited = new int[V];
            for (int i = 0; i < V; i++)
            {
                dist[i] = 0;
                prev[i] = -1; // no previous node
                visited[i] = -1; // not visited yet
            }

            PriorityQueue.PriorityQueueElement[] elements = new PriorityQueue.PriorityQueueElement[V];
            for (int i = 0; i < V; i++)
            {
                elements[i] = new PriorityQueue.PriorityQueueElement(i, int.MaxValue, i);
            }

            int id = 0;

            PriorityQueue pq = new PriorityQueue(V);
            pq.Insert(elements[u]);
            visited[u] = -2; // in queue

            while (!pq.IsEmpty())
            {
                u = pq.getMin();
                visited[u] = id++;
                if (u == v) break; // we arrived to our destination
                foreach (Edge e in adjacencyList[u])
                {
                    if (visited[e.v] == -1) // element not visited yet
                    {
                        visited[e.v] = -2;  // element in queue
                        elements[e.v].value = dist[u] + e.w;
                        pq.Insert(elements[e.v]);
                        dist[e.v] = dist[u] + e.w;
                        prev[e.v] = u;
                    }
                    else if (visited[e.v] == -2) // element already in the queue
                    {
                        int neww = dist[u] + e.w;
                        if (neww < dist[e.v]) // best new distance
                        {
                            elements[e.v].value = neww;
                            pq.Update(e.v);
                            dist[e.v] = neww;
                            prev[e.v] = u;
                        }
                    }
                }
            }

            if (u == v) // path found
            {
                while (prev[u] != -1)
                {
                    path.Add(name[u]);
                    u = prev[u];
                }
                path.Add(name[u]);
                path.Reverse();
            }

            return path;
        }

        // counts number of routes from the given city to the given city with less or equal journey time than the given number of days
        public int CountRoutesJourneyTime(string from, string to, int days)
        {
            if (!nodeName.ContainsKey(from) || !nodeName.ContainsKey(to))
                return -1;
            int u = nodeName[from];
            int v = nodeName[to];
            bool[] visited = new bool[V];
            return countRoutesJourneyTime(u, v, 0, days, visited);
        }

        // counts the number of routes from the given city to the given city with the given maximum of stops
        public int CountRoutesMaxStops(string from, string to, int maxStops, bool exactMatch)
        {
            if (!nodeName.ContainsKey(from) || !nodeName.ContainsKey(to))
                return -1;

            int u = nodeName[from];
            int v = nodeName[to];

            bool[] visited = new bool[V];
            return countRoutesMaxStops(u, v, 1, maxStops, exactMatch, visited);
        }

        int countRoutesMaxStops(int u, int v, int stops, int maxStops, bool exactMatch, bool[] visited)
        {
            if (stops > maxStops) return 0;

            if (u == v && exactMatch && maxStops == stops) return 1; // we arrived to the destination! count one more route

            int t = 0;
            if (u == v && !exactMatch) // we are in the destination
                t++;

            foreach (Edge e in adjacencyList[u])
            {
                if (!visited[e.v])
                {
                    visited[e.v] = true;
                    t += countRoutesMaxStops(e.v, v, stops + 1, maxStops, exactMatch, visited);
                    visited[e.v] = false;
                }
            }

            return t;
        }
        int countRoutesJourneyTime(int u, int v, int days, int maxDays, bool[] visited)
        {
            if (days > maxDays) return 0; // too much days
            int t = 0;
            if (u == v) t++;

            foreach (Edge e in adjacencyList[u])
            {
                if (!visited[e.v])
                {
                    visited[e.v] = true;
                    t += countRoutesJourneyTime(e.v, v, days + e.w, maxDays, visited);
                    visited[e.v] = false;
                }
            }
            return t;
        }
    }
}
