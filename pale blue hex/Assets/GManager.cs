using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GManager : MonoBehaviour
{


    [SerializeField]
    private GameObject[] panels;
    [SerializeField]
    private TMP_InputField IP;

    [SerializeField]
    private UIcontroller uic;

    [SerializeField]
    private bool isMainMenu;


    public Mirror.NetworkManager nm;
    // Start is called before the first frame update
    private void Start()
    {
        nm = GetComponent<Mirror.NetworkManager>();

        if (isMainMenu)
        {
            nm.networkAddress = "localhost";
            IP.text = nm.networkAddress;
        }

        Cursor.lockState = CursorLockMode.None;

        turnCounter = 0;
    }

    private void Update()
    {
        if (GetComponent<Mirror.NetworkManager>())
        {
            
        }
        
    }

    //Turn Management

    [SerializeField]
    public int turnCounter;

    [Mirror.Server]
    public void EndTurn()
    {
        turnCounter++;
    }

    //Network Manager

    public void StartHost()
    {
        nm.StartHost();
    }

    public void StartClient()
    {
        nm.networkAddress = IP.text;
        
        nm.StartClient();
    }

    public void StopClient()
    {
        nm.StopClient();
    }

    //UI

    public void PanelChanger(int p)
    {
        DeactivateAllPanels();
        panels[p].SetActive(true);
    }

    public void SceneChanger(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
    }

    public void DeactivateAllPanels()
    {
        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }
    }

    public void isMainMenuFalse()
    {
        isMainMenu = false;
    }
}
