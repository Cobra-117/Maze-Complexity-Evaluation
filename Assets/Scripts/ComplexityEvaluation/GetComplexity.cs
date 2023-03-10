using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComplexity : MonoBehaviour
{
    public MazeAlgo mazeAlgo;

    public int mazeWidth = 10;
    public int mazeLenght= 10;
    // Start is called before the first frame update
    void Start()
    {
        mazeAlgo.Generate(mazeWidth, mazeLenght);
        int[,] maze = mazeAlgo.GetMaze();
        Debug.Log("Cyclomatic complexity: " + 
        GetCyclomaticComplexity(maze).ToString());
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

    int GetSolutionLenght()
    {
        return 0;
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
        || tileValue == (int)MazeAlgo.TileTypes.NEW
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
        || tileValue == (int)MazeAlgo.TileTypes.NES
        || tileValue == (int)MazeAlgo.TileTypes.SW)
            return true;
        return false;
    }
}
