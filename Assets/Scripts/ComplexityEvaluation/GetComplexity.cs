using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetComplexity : MonoBehaviour
{
    public MazeAlgo mazeAlgo;

    public int mazeWidth = 10;
    public int mazeLenght= 10;
    bool measureMode = true;
    int[,] maze;
    int t1;
    int t2;
    // Start is called before the first frame update
    void Start()
    {
        long t1 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        mazeAlgo.Generate(mazeWidth, mazeLenght);
        long t2 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Debug.Log("Gen time: " + (t2 - t1).ToString());
        maze = mazeAlgo.GetMaze();
        Debug.Log("Cyclomatic complexity: " + 
        GetCyclomaticComplexity(maze).ToString());
    }

    void Update()
    {
        if (measureMode == true) {
            Debug.Log("Solution lenght: " + 
            GetSolutionLenght(maze).ToString());
            Debug.Log("Number of corridors:" +
            GetNumberofCorridors(maze).ToString());
            measureMode = false;
        }
    }

    int GetNumberofCorridors(int [,] maze)
    {
        int res = 0;

        for (int y = 0; y < maze.GetLongLength(0); y++) {
            for (int x = 0; x < maze.GetLongLength(1); x++) {
                if (maze[y,x] == (int)MazeAlgo.TileTypes.NE ||
                maze[y,x] == (int)MazeAlgo.TileTypes.NS||
                maze[y,x] == (int)MazeAlgo.TileTypes.NW ||
                maze[y,x] == (int)MazeAlgo.TileTypes.ES ||
                maze[y,x] == (int)MazeAlgo.TileTypes.EW ||
                maze[y,x] == (int)MazeAlgo.TileTypes.SW)
                    res += 1;
            }
        }
        return res;
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
            for (int y = 0; y < maze.GetLongLength(0); y++) {   
                for (int x = 0; x < maze.GetLongLength(1); x++) {
                    if (distanceMap[y, x] != currentDistance -1)
                        continue;
                    if (!HasNorthEdge(maze[y, x]) && distanceMap[y - 1, x] == -1) {
                        hasIndexed = true;
                        distanceMap[y - 1, x] = currentDistance;
                    }
                    if (!HasEastEdge(maze[y, x]) && distanceMap[y, x + 1] == -1) {
                        hasIndexed = true;
                        distanceMap[y, x + 1] = currentDistance;
                    }
                    if (!HasSouthEdge(maze[y, x]) && distanceMap[y + 1, x] == -1) {
                        hasIndexed = true;
                        distanceMap[y + 1, x] = currentDistance;
                    }
                    if (!HasWestEdge(maze[y, x]) && distanceMap[y, x - 1] == -1) {
                        hasIndexed = true;
                        distanceMap[y, x - 1] = currentDistance;
                    }
                }
            }
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
