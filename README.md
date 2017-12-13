# Shortest path Dijkstra Algorithm
An example on showing how to find the shortest path in a weighted network graph.
Solution: Visual Studio 2015 Project with no 3rd party dependency.

For all paths we assumed that we can only repeat the first city at the end of the path to close the cycle

We based all on a graph class which contains two representations of the graph. 
An adjacency matrix and an adjacency list. 
The adjacency matrix is used to check if a path is valid (and compute its length). 
and the adjacency list is used for Dijkstra algorithm (shortest path) and for computing the number of different routes in different questions.
For Dijkstra algorithm we implemented a priority queue class based on heaps.

The graph input is stored in a file "Data/network.txt" with the following format:

* Buenos Aires > New York = 6 days
* Buenos Aires > Casablanca = 5 days
* Buenos Aires > Cape Town = 4 days
* New York > Liverpool = 4 days
* Liverpool > Casablanca = 3 days
* Liverpool > Cape Town = 6 days
* Casablanca > Liverpool = 3 days
* Casablanca > Cape Town = 6 days
* Cape Town > New York = 8 days

Additional routes can be added.
Also the direct routes are in "Data/routes.txt"

Also the shortest path are in "Data/shortestPath.txt"

The unit test project is self explanatory with the test cases names. It covers the questions and some other related to input errors.

