using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{

    [SerializeField] private NetworkManagerPBH nm;

    [Header("UI")]
    [SerializeField] private TMP_InputField ipAddressInputField;
    [SerializeField] private Button joinButton;

    private void OnEnable()
    {
        NetworkManagerPBH.OnClientConnected += HandleClientConnected;
        NetworkManagerPBH.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerPBH.OnClientConnected -= HandleClientConnected;
        NetworkManagerPBH.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        //sets the network address field in the network manager to whatever the user put in as the IP
        nm.networkAddress = ipAddressInputField.text;
        nm.StartClient();

        //disable the button while trying to connect
        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        //if we connect successfully re-enable the button
        joinButton.interactable = true;
    }

    private void HandleClientDisconnected()
    {
        //on failure to connect also re-enable the button
        joinButton.interactable = true;

        Debug.Log("FAILED TO CONNECT TO SERVER");
    }
}
