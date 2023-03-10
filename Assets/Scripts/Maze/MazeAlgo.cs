using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class MazeAlgo : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase[] tiles;
    public enum TileTypes 
    {
        empty = 0, NESW = 15,
        W = 1, S = 2, E = 4, N = 8,
        SW = 3, EW = 5, ES = 6, NW = 9, 
        NS = 10, NE = 12,
        ESW = 7, NSW = 11, NEW = 13, NES = 14
    };

    public int[,] maze;
    public abstract void Generate(int width, int lenght);
    public abstract void FillMaze(int height, int width);

    protected void DisplayMaze()
    {
        for (int y = 0; y < maze.GetLongLength(0); y++) {
            for (int x = 0; x < maze.GetLongLength(1); x++) {
                tilemap.SetTile(new Vector3Int(x, -y, 0), tiles[maze[y, x]]);
            }
        }
    }

    public int[,] GetMaze() { return maze;}
}
