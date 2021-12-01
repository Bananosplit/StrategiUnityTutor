using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {
    [SerializeField]
    GameObject ground;

    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private GameTile tile;

    GameTile[] tiles;
    Queue<GameTile> searchFrontier = new Queue<GameTile>();

    public void Initialization(Vector2Int size) {
        this.size = size;
        ground.transform.localScale = new Vector3(size.x, size.y, 1);
        tiles = new GameTile[size.x * size.y];
        CreateTileOnGround();

    }

    void CreateTileOnGround() {
        var offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);
        for (int y = 0, i = 0; y < size.x; y++) {
            for (int x = 0; x < size.y; x++, i++) {
                GameTile tile = tiles[i] = Instantiate(this.tile);
                tile.transform.SetParent(this.transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0.002f, y - offset.y);

                if (x > 0) GameTile.MakeEastWestNeihbor(tiles[i], tiles[i - 1]);
                if (y > 0) GameTile.MakeNorthSouthNeihbor(tiles[i], tiles[i - size.x]);
            }
        }
        FindPath();

    }

    public void FindPath() {
        foreach (var tile in tiles) {
            tile.ClearPath();
        }

        int destinationIndex = tiles.Length / 2;
        tiles[destinationIndex].BecomeDestination();
        searchFrontier.Enqueue(tiles[destinationIndex]);

        while (searchFrontier.Count > 0) {
            var tile = searchFrontier.Dequeue();
            if (tile != null) {
                searchFrontier.Enqueue(tile.GrowToNorth());
                searchFrontier.Enqueue(tile.GrowToEast());
                searchFrontier.Enqueue(tile.GrowToSouth());
                searchFrontier.Enqueue(tile.GrowToWest());
            }
        }

        foreach (var tile in tiles)
            tile.ShowPath();
    }

}
