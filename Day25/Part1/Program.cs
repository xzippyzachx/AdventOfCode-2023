// Day 25
// Part 1

string[] lines = File.ReadAllLines("Part1/input.txt");

Dictionary<string, HashSet<string>> connections = new Dictionary<string, HashSet<string>>();

foreach (string line in lines)
{
    string[] split = line.Split(": ");
    string comp = split[0];
    HashSet<string> cons = split[1].Split(" ").ToHashSet();

    if (!connections.ContainsKey(comp))
    {
        connections.Add(comp, cons);

        foreach (string con in cons)
        {
            if (!connections.ContainsKey(con))
            {
                connections.Add(con, new HashSet<string>() { comp });
            }
            else
            {
                connections[con].Add(comp);
            }
        }
    }
    else
    {
        foreach (string con in cons)
        {
            connections[comp].Add(con);

            if (!connections.ContainsKey(con))
            {
                connections.Add(con, new HashSet<string>() { comp });
            }
            else
            {
                connections[con].Add(comp);
            }
        }
    }
}

// foreach (KeyValuePair<string, HashSet<string>> connection in connections)
// {
//     Console.WriteLine(connection.Key + ": " + string.Join(",", connection.Value));
// }

List<string> FindPath(string start, string end)
{
    Queue<Node> nextNodes = new Queue<Node>();
    nextNodes.Enqueue(new Node(start, 0, new List<string>() { start }));
    while(nextNodes.TryDequeue(out Node? node))
    {
        foreach (string con in connections[node.curr])
        {
            int newDist = node.dist + 1;

            if (node.visited.Contains(con))
            {
                continue;
            }

            List<string> newVisited = new List<string>(node.visited) { con };

            if (con == end)
            {
                //Console.WriteLine(string.Join(",", newVisited));
                return newVisited;
            }

            nextNodes.Enqueue(new Node(con, newDist, newVisited));
        }
    }

    return new List<string>();
}

// Simple dijkstra's algorithm to make random paths
Dictionary<Edge, int> edges = new Dictionary<Edge, int>();
for (int a = 0; a < 100; a++)
{
    Random rnd = new Random();
    string start = connections.Keys.ToArray()[rnd.Next(0, connections.Keys.Count)];
    string end = connections.Keys.ToArray()[rnd.Next(0, connections.Keys.Count)];

    List<string> visted = FindPath(start, end);
    string last = "";
    foreach (string comp in visted)
    {
        if (last != "")
        {
            Edge edge = new Edge(last, comp);

            Edge reversedEdge = new Edge(comp, last);

            if (!edges.ContainsKey(edge) && !edges.ContainsKey(reversedEdge))
            {
                edges.Add(edge, 1);
            }
            else if (edges.ContainsKey(edge))
            {
                edges[edge]++;
            }
            else
            {
                edges[reversedEdge]++;
            }
        }
        last = comp;
    }
    Console.WriteLine(a + 1 + "/" + 100);
}

// foreach (Edge edge in edges.Keys)
// {
//     Console.WriteLine(edge + ": " + edges[edge]);
// }

// Find top 3 edges that are crossed the most
List<Edge> cutEdges = new List<Edge>();
for (int i = 0; i < 3; i++)
{
    int count = 0;
    Edge cutEdge = edges.Keys.ToArray()[0];
    foreach (Edge edge in edges.Keys)
    {
        if (edges[edge] > count)
        {
            count = edges[edge];
            cutEdge = edge;
        }
    }
    cutEdges.Add(cutEdge);
    edges.Remove(cutEdge);
}

foreach (Edge edge in cutEdges)
{
    Console.WriteLine(edge);

    connections[edge.nodeA].Remove(edge.nodeB);
    connections[edge.nodeB].Remove(edge.nodeA);
}

// BFS on one group
string startNode = connections.Keys.ToArray()[0];
Queue<string> nextNodes = new Queue<string>();
nextNodes.Enqueue(startNode);
HashSet<string> visited = new HashSet<string>();
while(nextNodes.TryDequeue(out string? curr))
{
    foreach (string con in connections[curr])
    {
        if (visited.Contains(con))
        {
            continue;
        }
        
        visited.Add(con);
        nextNodes.Enqueue(con);
    }
}

Console.WriteLine("GroupA: " + visited.Count);
Console.WriteLine("GroupB: " + (connections.Count - visited.Count));

Console.WriteLine(visited.Count * (connections.Count - visited.Count));

record Edge(string nodeA, string nodeB);
record Node(string curr, int dist, List<string> visited);