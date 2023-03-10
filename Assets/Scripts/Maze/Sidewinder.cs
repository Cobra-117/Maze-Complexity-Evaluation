using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidewinder : MazeAlgo
{
public enum Directions
    {
        NorthDir = 0,
        WestDir = 1
    };

    void Start()
    {
        Generate(20, 20);
    }

    public override void Generate(int height, int width)
    {
        FillMaze(height, width);
        
        
        for (int y = 0; y < height; y++) 
        {
            List<int> carvedTiles = new List<int>();
            for (int x = 0; x < width; x++) {
                if (y == 0 && x + 1 < width)
                    Carve(y, x, carvedTiles);
                else if (x + 1 < width && Random.Range(0, 2) == 0)
                    carvedTiles = Carve(y, x, carvedTiles);
                else if (y >0) {
                    OpenNorth(y, x, carvedTiles);
                    carvedTiles = new List<int>();
                }
            }
        }
        DisplayMaze();
    }

    public List<int> Carve(int y, int x, List<int> carvedTiles)
    {
        maze[y , x] -= (int)TileTypes.E;
        maze[y, x + 1] -= (int)TileTypes.W;
        carvedTiles.Add(x);
        return carvedTiles;
    }

    public void OpenNorth(int y, int x, List<int> carvedTiles)
    {
        int openingX;
        if (carvedTiles.Count == 0)
            openingX = x;
        else {
            openingX = Random.Range(carvedTiles[0], carvedTiles[carvedTiles.Count - 1] + 1);
            Debug.Log("min : " + carvedTiles[0].ToString());
            Debug.Log("min : " + carvedTiles[carvedTiles.Count - 1]);
        }
        maze[y , openingX] -= (int)TileTypes.N;
        maze[y - 1, openingX] -= (int)TileTypes.S;
    }

    public override void FillMaze(int height, int width)
    {
        maze = new int[height,width];
        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) {
                maze[y, x] = (int)TileTypes.NESW;
            }
        }
    }
}
