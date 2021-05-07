using Mirror;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerPBH : NetworkManager
{
    //VERY USEFUL FOR SCENE CHANGING
    //REFRENCES SCENE by name by dragging it in through the inspector
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerPBH roomPlayerPrefab;//the lobby player prefab

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;


    //these two just make sure all gameobjects are under the spawnable prefabs so we dont have to add them manually
    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach(var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
    }

    
    //if client connects do the normal stuff and ping the event
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }
    //if client disconnects - same
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //If too many players are already connected - disconnect
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
        //if we are trying to connect to the wrong scene (ie not in lobby) - disconnect
        if("Assets/Scenes/" + SceneManager.GetActiveScene().name + ".unity" != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if ("Assets/Scenes/" + SceneManager.GetActiveScene().name + ".unity" == menuScene)//if in the lobby
        {
            NetworkRoomPlayerPBH roomPlayerInstance = Instantiate(roomPlayerPrefab);//add an instance of the room player prefab

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);//tie together the connection and the gameobject
        }
    }
}
