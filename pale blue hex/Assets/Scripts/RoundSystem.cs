using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mirror;
using TMPro;

public class RoundSystem : NetworkBehaviour
{
    [SyncVar] [SerializeField] private int roundNumber = 1;//starts on round 1 / round 10 planets blow up
    [SyncVar] [SerializeField] private int roundPhase = 0; //phase 0 -> getting money/phase 1 -> spending money/phase 2 -> moving

    private NetworkGamePlayerPBH player;
    private NetworkManagerPBH room;
    private NetworkManagerPBH Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerPBH;
        }
    }

    [Header("UI")]
    [SerializeField] Button readyButton;

    #region Server

    public override void OnStartServer()
    {
        //TODO - cleanup
        //NetworkManagerPBH.OnServerReadied += CheckToStartRound;
    }

    [Server]
    public void CheckToStartGame()
    {
        if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count)
        {
            Debug.Log("Waiting for " + (Room.GamePlayers.Count(x => x.connectionToClient.isReady) - Room.GamePlayers.Count)+ " players to be ready.");
            return;
        }

        Debug.Log("All players are ready, starting game.");
        RpcStartGame();
    }

    [Server]
    public void checkToNextPhase()
    {
        if(Room.GamePlayers.Count(x => x.finishedRound) != Room.GamePlayers.Count)
        {
            Debug.Log("Waiting for " + (Room.GamePlayers.Count(x => x.finishedRound) - Room.GamePlayers.Count));
            return;
        }

        Debug.Log("All players are ready, moving to next phase");

        roundPhase++;
        if(roundPhase == 3)
        {
            roundNumber++;
            roundPhase = 0;
        }

        //call some event / update ui
    }

    #endregion

    #region Client

    [ClientRpc]
    private void RpcStartGame()
    {
        Debug.Log("Starting Game");

        readyButton.interactable = true;
    }

    public override void OnStartClient()
    {
        foreach(NetworkGamePlayerPBH p in Room.GamePlayers)
        {
            if (p.isLocalPlayer)
            {
                player = p;
            }
        }

        player.CmdCheckReady(this);
        base.OnStartClient();
    }

    #endregion

    
}
