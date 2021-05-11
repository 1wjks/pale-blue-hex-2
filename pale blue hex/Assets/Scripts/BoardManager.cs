// Fills tilemap area with checkerboard pattern of tileA and tileB
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    public TileBase spaceHex;

    public Tilemap map;

    public int radius;

    public int players;

    private int num_tiles;

    // public TileBase launchHex;
    // public TileBase baseworldHex;
    void Start()
    {
        num_tiles = calculate_tiles(radius);
        Debug.Log("Number of tiles: " + num_tiles);
        map.ClearAllTiles();

        // Vector3Int[] positions = new Vector3Int[num_tiles];
        // TileBase[] tileArray = new TileBase[num_tiles];

        map.SetTile(new Vector3Int(0, 0, 0), spaceHex);

        Vector3Int[] dirs = directions(players);

        for(int x = 1; x < radius; x++)
        {
            foreach (Vector3Int dir in dirs) {
                map.SetTile(dir * x, spaceHex);
            }
        }

    }

    Vector3Int[] directions(int players) {
        if (players == 2)
            return new Vector3Int[] {new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0)};
        else if (players == 3)
            return new Vector3Int[] {new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)};
        else if (players == 4)
            return new Vector3Int[] {new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 0, 0)};
        else if (players == 5)
            return new Vector3Int[] {new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 1, 0)};
        else if (players == 6)
            return new Vector3Int[] {new Vector3Int(-1, 0, 0), new Vector3Int(-1, 1, 0), new Vector3Int(-1, 1, 0), new Vector3Int(1, -1, 0), new Vector3Int(1, 1, 0), new Vector3Int(1, 0, 0)};
        else
            // TODO - Replace this with an error throw
            return new Vector3Int[] {};
    }

    int calculate_tiles(int radius) {
        return 1 - 3 * radius + 3 * radius * radius ;
    }
}