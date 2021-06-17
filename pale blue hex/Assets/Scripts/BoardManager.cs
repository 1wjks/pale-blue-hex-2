// Fills tilemap area with checkerboard pattern of tileA and tileB
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;

public class BoardManager : MonoBehaviour
{
    public TileBase spaceHex;
    public TileBase planetHex;

    public Tilemap map;

    [SerializeField] private int radius;

    [SerializeField] private int players;

    [SerializeField] private int num_tiles;

    private Grid grid;

    [SerializeField] private NetworkManagerPBH NetworkManager;

    // public TileBase launchHex;
    // public TileBase baseworldHex;
    void Start()
    {
        //find the NetworkManager component on the gameManager
        if (GameObject.Find("GameManager") != null)
        {
            NetworkManager = GameObject.Find("GameManager").GetComponent<NetworkManagerPBH>();
            players = NetworkManager.GamePlayers.Count; //set the number of players connected to the tile number
        }
        

        num_tiles = calculate_tiles(radius);
        grid = map.layoutGrid;

        map.ClearAllTiles();

        map.SetTile(new Vector3Int(0, 0, 0), spaceHex);

        FillEmptyBoard(radius);
        PlacePlanets(radius, players);

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

    private Vector3 SecondaryDirection(Vector3 d)
    {
        if (d == Directions.U) { return Directions.UR; }
        if (d == Directions.UR) { return Directions.DR; }
        if (d == Directions.DR) { return Directions.D; }
        if (d == Directions.D) { return Directions.DL; }
        if (d == Directions.DL) { return Directions.UL; }
        if (d == Directions.UL) { return Directions.U; }

        return Vector3.zero;
    }

    private void FillEmptyBoard(int radius)
    {
        Vector3 sd;

        for (int x = 1; x < radius; x++)
        {
            for (int y = 0; y < radius-x
; y++)
            {
                foreach (Vector3 dir in directions(6))//all primary directions
                {
                    sd = SecondaryDirection(dir);
                    map.SetTile(grid.LocalToCell(dir * x * 0.866f + sd * y * 0.866f), spaceHex);
                    Debug.Log("Radius " + x + " | Cell " + grid.LocalToCell(dir * x));
                }
            }
        }
    }

    private void PlacePlanets(int radius, int players)
    {
        foreach(Vector3 dir in directions(players))
        {
            map.SetTile(grid.LocalToCell(dir * (radius - 1) * 0.866f), planetHex);
            Debug.Log("Radius " + (radius - 1) + " | Cell " + grid.LocalToCell(dir * (radius - 1)));
        }
    }
}