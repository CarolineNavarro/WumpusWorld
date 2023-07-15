using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManeger : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    
    [SerializeField] private Transform _cam;

    [SerializeField] private float _tileSize;

    private Dictionary<Vector2, Tile> _tiles;

    [SerializeField] private Tile[] _tilesBase;

    private int cont;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                
                var spawnedTile = Instantiate(_tilesBase[cont], new Vector3(x, y), Quaternion.identity);
                
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.transform.localScale = Vector3.one *_tileSize;
               
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(x, y)] = spawnedTile;
              
                cont++;
               
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
  
}
