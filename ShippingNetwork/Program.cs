using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingNetworkLibrary;

namespace ShippingNetWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = Graph.LoadGraph("Data/network.txt");

            printTotalJourneyDays(g);
            Console.WriteLine();
            printShortestPaths(g);
            Console.WriteLine();
            printTotalRoutesWithMaxStops(g);
            Console.WriteLine();
            printTotalRoutesWithExactStops(g);
            Console.WriteLine();
            printJourneyTimeIsExactlyOrLessThan(g);
            Console.WriteLine();

            Console.ReadLine();

        }
        private static void printTotalJourneyDays(Graph g)
        {
            Console.WriteLine("*****Total Journey Days******");

            List<string> pathsList = Graph.LoadRoutes("Data/routes.txt");
            int totalJourneyDays = 0;

            // direct routes
            foreach (var path in pathsList)
            {
                string[] arrPath = path.Split('>');
                totalJourneyDays = g.GetJourneyTime(arrPath);
                Console.WriteLine("The total journey time for the following direct routes "
                    + string.Join(" > ", arrPath) + " is: "
                    + (totalJourneyDays == -1 ? "In-valid route" : totalJourneyDays.ToString() + " days"));
            }

        }
        private static void printShortestPaths(Graph g)
        {
            Console.WriteLine("*****Shortest journey path******");
            // Shortest journey path
            List<string> shortestPathsList = Graph.LoadRoutes("Data/shortestPath.txt");
            foreach (var path in shortestPathsList)
            {
                string[] arrPath = path.Split('>');
                if (arrPath.Length == 2)
                {
                    List<string> shortestPath = g.GetShortestPath(arrPath[0], arrPath[1]);
                    if (shortestPath != null)
                        Console.WriteLine("The shortest path from " + arrPath[0] + " to " + arrPath[1] + " is: "
                            + string.Join(" > ", shortestPath)
                            + ". Total days of " + g.GetJourneyTime(shortestPath.ToArray()));
                    else
                        Console.WriteLine("Cannot calculate shortest path: Invalid node names.");
                }
                else
                {
                    Console.WriteLine("Invalid shortest path argument exists.");
                }
            }
        }
        private static void printTotalRoutesWithMaxStops(Graph g)
        {
            Console.WriteLine("*****Max stops from to******");
            // Max stops
            int result = g.CountRoutesMaxStops("Liverpool", "Liverpool", 3, false);
            Console.WriteLine("The number of routes from Liverpool to Liverpool with a maximum number " +
                "of 3 stops are: " + result);
        }
        private static void printTotalRoutesWithExactStops(Graph g)
        {
            Console.WriteLine("*****Exact stops from to******");
            // Exact stops
            int result = g.CountRoutesMaxStops("Buenos Aires", "Liverpool", 4, true);
            Console.WriteLine("The number of routes from Buenos Aires to Liverpool where " +
                "exactly 4 stops are made is: " + (result == -1 ? "Unknown" : result.ToString()));
        }
        private static void printJourneyTimeIsExactlyOrLessThan(Graph g)
        {
            Console.WriteLine("*****Journey time is exactly or less******");
            // Exact stops

            int result = g.CountRoutesJourneyTime("Liverpool", "Liverpool", 25);
            Console.WriteLine("The number of routes from Liverpool to Liverpool where the " +
                "journey time is less than or equal to 25 days: " + (result == -1 ? "Unknown" : result.ToString()));
        }
    }
}
