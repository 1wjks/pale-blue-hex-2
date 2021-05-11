// Fills tilemap area with checkerboard pattern of tileA and tileB
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    public TileBase spaceHex;

    public Tilemap map;

    public int radius;

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

        for(int x=-1; x<= 1; x++)
        {
            for(int y=-1; y<= 1; y++)
            {
                map.SetTile(new Vector3Int(x, y, 0), spaceHex);
            }

        }
    }

    int calculate_tiles(int radius) {
        return 1 - 3 * radius + 3 * radius * radius ;
    }
}