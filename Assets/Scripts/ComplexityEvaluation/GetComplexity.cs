using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComplexity : MonoBehaviour
{
    public MazeAlgo mazeAlgo;

    public int mazeWidth = 10;
    public int mazeLenght= 10;
    bool debug = true;
    int[,] maze;
    // Start is called before the first frame update
    void Start()
    {
        mazeAlgo.Generate(mazeWidth, mazeLenght);
        maze = mazeAlgo.GetMaze();
        Debug.Log("Cyclomatic complexity: " + 
        GetCyclomaticComplexity(maze).ToString());
        /*Debug.Log("Solution lenght: " + 
        GetSolutionLenght(maze).ToString());*/
    }

    void Update()
    {
        if (debug == true) {
            Debug.Log("Solution lenght: " + 
            GetSolutionLenght(maze).ToString());
            debug = false;
        }
    }
    int GetCyclomaticComplexity(int[,] maze)
    {
        // E – N + 2, where E = number of edges, N = number of nodes
        int edges = 0;
        int nodes = maze.Length;
        for (int y = 0; y < maze.GetLongLength(0); y++) {
            for (int x = 0; x < maze.GetLongLength(1); x++) {
                if (!HasNorthEdge(maze[y, x])) { edges +=1;}
                if (!HasEastEdge(maze[y, x])) { edges +=1;}
                if (!HasSouthEdge(maze[y, x])) { edges +=1;}
                if (!HasWestEdge(maze[y, x])) { edges +=1;}
            }
        }
        return edges - nodes + 2;
    }

    int GetSolutionLenght(int[,] maze)
    {
        return (int)GetDistanceMap(maze)[0, 0];
    }

    float[,] GetDistanceMap(int[,] maze)
    {
        float [,] distanceMap = new float
        [maze.GetLongLength(0), maze.GetLongLength(1)];
        for (int y = 0; y < maze.GetLongLength(0); y++) {
            for (int x = 0; x < maze.GetLongLength(1); x++) {
                distanceMap[y, x] = -1;
            }
        }

        bool hasIndexed = true;
        int currentDistance = 0;
        distanceMap[maze.GetLongLength(0) -1, maze.GetLongLength(1) - 1] = 0;
        while (hasIndexed == true) {
            hasIndexed = false;
            currentDistance += 1;
            Debug.Log("Indexing: " + currentDistance.ToString());
            for (int y = 0; y < maze.GetLongLength(0); y++) {   
                for (int x = 0; x < maze.GetLongLength(1); x++) {
                    if (distanceMap[y, x] != currentDistance -1)
                        continue;
                    //Debug.Log("Found end: ");
                    if (!HasNorthEdge(maze[y, x]) && distanceMap[y - 1, x] == -1) {
                        hasIndexed = true;
                        distanceMap[y - 1, x] = currentDistance;
                        Debug.Log("Indexed North: " + (y - 1).ToString() + " " + x.ToString());
                    }
                    if (!HasEastEdge(maze[y, x]) && distanceMap[y, x + 1] == -1) {
                        hasIndexed = true;
                        distanceMap[y, x + 1] = currentDistance;
                        Debug.Log("Indexed East: " + (y).ToString() + " " + (x+1).ToString());
                    }
                    if (!HasSouthEdge(maze[y, x]) && distanceMap[y + 1, x] == -1) {
                        hasIndexed = true;
                        distanceMap[y + 1, x] = currentDistance;
                        Debug.Log("Indexed South: " + (y+1).ToString() + " " + (x).ToString());
                    }
                    if (!HasWestEdge(maze[y, x]) && distanceMap[y, x - 1] == -1) {
                        hasIndexed = true;
                        distanceMap[y, x - 1] = currentDistance;
                        Debug.Log("Indexed West: " + (y).ToString() + " " + (x-1).ToString());
                    }
                }
            }
            //return distanceMap;
        }
        return distanceMap;
    }

    bool HasNorthEdge(int tileValue) 
    {
        if (tileValue == (int)MazeAlgo.TileTypes.N
        || tileValue == (int)MazeAlgo.TileTypes.NE
        || tileValue == (int)MazeAlgo.TileTypes.NES
        || tileValue == (int)MazeAlgo.TileTypes.NESW
        || tileValue == (int)MazeAlgo.TileTypes.NEW
        || tileValue == (int)MazeAlgo.TileTypes.NS
        || tileValue == (int)MazeAlgo.TileTypes.NSW
        || tileValue == (int)MazeAlgo.TileTypes.NW)
            return true;
        return false;
    }

    bool HasEastEdge(int tileValue) 
    {
        if (tileValue == (int)MazeAlgo.TileTypes.E
        || tileValue == (int)MazeAlgo.TileTypes.NE
        || tileValue == (int)MazeAlgo.TileTypes.NES
        || tileValue == (int)MazeAlgo.TileTypes.NESW
        || tileValue == (int)MazeAlgo.TileTypes.NEW
        || tileValue == (int)MazeAlgo.TileTypes.EW
        || tileValue == (int)MazeAlgo.TileTypes.ESW
        || tileValue == (int)MazeAlgo.TileTypes.ES)
            return true;
        return false;
    }

    bool HasSouthEdge(int tileValue) 
    {
        if (tileValue == (int)MazeAlgo.TileTypes.S
        || tileValue == (int)MazeAlgo.TileTypes.NS
        || tileValue == (int)MazeAlgo.TileTypes.NES
        || tileValue == (int)MazeAlgo.TileTypes.NESW
        || tileValue == (int)MazeAlgo.TileTypes.NSW
        || tileValue == (int)MazeAlgo.TileTypes.ES
        || tileValue == (int)MazeAlgo.TileTypes.ESW
        || tileValue == (int)MazeAlgo.TileTypes.SW)
            return true;
        return false;
    }

    bool HasWestEdge(int tileValue) 
    {
        if (tileValue == (int)MazeAlgo.TileTypes.W
        || tileValue == (int)MazeAlgo.TileTypes.NW
        || tileValue == (int)MazeAlgo.TileTypes.NEW
        || tileValue == (int)MazeAlgo.TileTypes.NESW
        || tileValue == (int)MazeAlgo.TileTypes.EW
        || tileValue == (int)MazeAlgo.TileTypes.SW
        || tileValue == (int)MazeAlgo.TileTypes.NSW
        || tileValue == (int)MazeAlgo.TileTypes.ESW)
            return true;
        return false;
    }
}
