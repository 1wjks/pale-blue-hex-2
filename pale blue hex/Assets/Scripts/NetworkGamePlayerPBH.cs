using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NetworkGamePlayerPBH : NetworkBehaviour
{

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

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

}
