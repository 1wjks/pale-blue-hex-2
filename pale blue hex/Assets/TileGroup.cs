using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TileGroup : NetworkBehaviour
{

    [SyncVar] SyncDictionary<string, int> tileset = new SyncDictionary<string, int>();//dictionary containing the tile name and the number of those tiles in the bag
    [SyncVar] string lastPickedTile;
    private List<string> orderedKeys = new List<string>();// to insure the PickTile function always pulls each tile the same way every time
    [SerializeField] private int numTiles;

    private void Start()
    {
        //init the dictionary and add all base tiles

        tileset.Add("EmptySpaceTile", 20);
        orderedKeys.Add("EmptySpaceTile");
        tileset.Add("BlackHoleTile", 5);
        orderedKeys.Add("BlackHoleTile");

        FindNumTiles(tileset);
        //possible custom tile number adjuster later
        //ie at game start you can choose how many of each tile there is
    }

    private void FindNumTiles(SyncDictionary<string, int> d)
    {
        numTiles = 0;
        foreach(KeyValuePair<string, int> entry in tileset)
        {
            numTiles += entry.Value;
        }
        Debug.Log("There are " + numTiles + " tiles in the dictionary at start.");
    }

    [Server]
    public void PickTile()
    {
        int random = Random.Range(0, numTiles) + 1;// generates a random number between 1 and the number of tiles
        int sum = 0;
        foreach(string key in orderedKeys)
        {
            sum += tileset[key];
            if(random <= sum)
            {
                lastPickedTile = key;
                tileset[key]--;
                RpcTileUpdated();
                return;
            }
        }
        Debug.Log("Unreachable state: PickTile() in TileGroup has failed.");
        lastPickedTile = "nullTile";
    }

    [ClientRpc]
    public void RpcTileUpdated()
    {
        //let the client know that lastPickedTile has been updated to the latest tile
        //do whatever with the newly picked tile

    }

}
