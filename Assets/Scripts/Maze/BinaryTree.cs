using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MazeAlgo
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
            for (int x = 0; x < width; x++) {
                List<int> possibleDirs = new  List<int>();
                int choosedDir = -1;

                if (y > 0) {
                    possibleDirs.Add((int)Directions.NorthDir);
                }
                if (x > 0) {
                    possibleDirs.Add((int)Directions.WestDir);
                }
                if (possibleDirs.Count > 1)
                    choosedDir = Random.Range(0, possibleDirs.Count);
                else if ((possibleDirs.Count > 0))
                    choosedDir = possibleDirs[0];
                if (choosedDir == (int)Directions.NorthDir) {
                    maze[y , x] -= (int)TileTypes.N;
                    maze[y - 1, x] -= (int)TileTypes.S;
                }
                else if (choosedDir == (int)Directions.WestDir) {
                    maze[y , x] -= (int)TileTypes.W;
                    maze[y, x - 1] -= (int)TileTypes.E;
                }
            }
        }
        DisplayMaze();
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
