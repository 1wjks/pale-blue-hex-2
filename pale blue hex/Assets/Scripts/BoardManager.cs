// Fills tilemap area with checkerboard pattern of tileA and tileB
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    public TileBase spaceHex;

    public Tilemap map;

    [SerializeField] private int radius;

    [SerializeField] private int players;

    [SerializeField] private int num_tiles;

    private Grid grid;

    // public TileBase launchHex;
    // public TileBase baseworldHex;
    void Start()
    {
        num_tiles = calculate_tiles(radius);
        grid = map.layoutGrid;

        map.ClearAllTiles();

        map.SetTile(new Vector3Int(0, 0, 0), spaceHex);

        Vector3[] dirs = directions(players);

        for(int x = 1; x < radius; x++)
        {
            foreach (Vector3 dir in dirs) {
                map.SetTile(grid.LocalToCell(dir * x * 0.866f), spaceHex);
                Debug.Log("Radius " + x + " | Cell " + grid.LocalToCell(dir * x));
            }
        }

    }

    Vector3[] directions(int players) {
        if (players == 2)
            return new Vector3[] {Directions.U, Directions.D};
        else if (players == 3)
            return new Vector3[] {Directions.D, Directions.UR, Directions.UL};
        else if (players == 4)
            return new Vector3[] {Directions.D, Directions.DL, Directions.U, Directions.UR};
        else if (players == 5)
            return new Vector3[] {Directions.D, Directions.DL, Directions.DR, Directions.UL, Directions.UR};
        else if (players == 6)
            return new Vector3[] {Directions.D, Directions.DL, Directions.DR, Directions.U, Directions.UL, Directions.UR};
        else
            // TODO - Replace this with an error throw
            return new Vector3[] {};
    }

    int calculate_tiles(int radius) {
        return 1 - 3 * radius + 3 * radius * radius ;
    }
}