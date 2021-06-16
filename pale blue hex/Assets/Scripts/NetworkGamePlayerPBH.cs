using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NetworkGamePlayerPBH : NetworkBehaviour
{
    [Header("Resources")]
    public int moolah = 0;
    public int metal = 0;
    public int crystal = 0;

    public bool finishedRound = false;

    [SyncVar]
    private string displayName = "Loading...";

    private NetworkManagerPBH room;
    private NetworkManagerPBH Room
    {
        get
        {
            if(room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerPBH;
        }
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Room.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Room.GamePlayers.Remove(this);
    }

    [Command]
    public void CmdCheckReady(RoundSystem rs)//tell the server to check if everyone is ready
    {
        Debug.Log("Checking ready");
        rs.CheckToStartGame();
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public void readyButtonFunction()
    {
        finishedRound = !finishedRound;
    }

}
