using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MazeAlgo
{
    public List<Vector2Int> visitedTiles = new List<Vector2Int>();
    Vector2Int currentTile;
    public enum Directions
    {
        NorthDir = 0,
        EastDir = 1,
        SouthDir = 2,
        WestDir = 3
    }

    void Start()
    {
        //Generate(20, 20);
    }

    public override void Generate(int height, int width)
    {
        FillMaze(height, width);
        currentTile = new Vector2Int(0, 0);
        visitedTiles.Add(currentTile);
        while (true) {
            while (OpenRandomNeighbor(height, width));
            Goback();
            if (currentTile.x == 0 && currentTile.y == 0) {
                DisplayMaze();
                return;
            }
        }
        
    }

    public bool Goback()
    {
        for (int i = 0; i < visitedTiles.Count; i++) 
        {
            currentTile = visitedTiles[visitedTiles.Count - 1];
            if (GetAvNeighbors(currentTile.y, currentTile.x).Count != 0) {
                return true;
            }
            visitedTiles.RemoveAt(visitedTiles.Count - 1);
        }
        return false;
    }

    public bool OpenRandomNeighbor(int height, int width)
    {
        List<Directions> avNeighbors = GetAvNeighbors(height, width);

        if (avNeighbors.Count == 0)
            return false;
        int choosenNeighbor = (int)avNeighbors[Random.Range(0, avNeighbors.Count)];
        switch (choosenNeighbor)
        {
            case 0: 
                maze[(int)currentTile.y, (int)currentTile.x] -= (int)TileTypes.N;
                maze[(int)currentTile.y - 1, (int)currentTile.x] -= (int)TileTypes.S;
                currentTile.y -= 1;
                break;
            case 3: 
                maze[(int)currentTile.y, (int)currentTile.x] -= (int)TileTypes.W;
                maze[(int)currentTile.y, (int)currentTile.x - 1] -= (int)TileTypes.E;
                currentTile.x -= 1;
                break;
            case 2: 
                maze[(int)currentTile.y, (int)currentTile.x] -= (int)TileTypes.S;
                maze[(int)currentTile.y + 1, (int)currentTile.x] -= (int)TileTypes.N;
                currentTile.y += 1;
                break;
            case 1: 
                maze[(int)currentTile.y, (int)currentTile.x] -= (int)TileTypes.E;
                maze[(int)currentTile.y, (int)currentTile.x + 1] -= (int)TileTypes.W;
                currentTile.x += 1;
                break;
            default: break;
        }
        visitedTiles.Add(currentTile);
        return true;
    }

    public List<Directions> GetAvNeighbors(int height, int width)
    {
        List<Directions> avNeighbors = new List<Directions>();

        if (currentTile.y > 0 && maze[(int)currentTile.y -1, 
        (int)currentTile.x] == (int)TileTypes.NESW)
            avNeighbors.Add(Directions.NorthDir);
        
        if (currentTile.x > 0 && maze[(int)currentTile.y, 
        (int)currentTile.x - 1] == (int)TileTypes.NESW)
            avNeighbors.Add(Directions.WestDir);
        
        if (currentTile.y + 1 < height && 
        maze[(int)currentTile.y + 1, 
        (int)currentTile.x] == (int)TileTypes.NESW)
            avNeighbors.Add(Directions.SouthDir);
        
        if (currentTile.x + 1 < width && 
        maze[(int)currentTile.y, 
        (int)currentTile.x + 1] == (int)TileTypes.NESW)
            avNeighbors.Add(Directions.EastDir);
        return avNeighbors;
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
