using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GManager : MonoBehaviour
{

    [SerializeField] private UIcontroller uic;

    [SerializeField] private bool isMainMenu;

    [Header("UI")]
    [SerializeField] private GameObject[] panels;
    [SerializeField] private TMP_InputField IP;

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

    //Host

    public void HostLobby()
    {
        nm.StartHost();
    }

    //UI

    //sets all panels to inactive then activates the one called
    public void PanelChanger(int p)
    {
        DeactivateAllPanels();
        panels[p].SetActive(true);
    }

    //just a scenechanging method
    public void SceneChanger(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
    }

    //sets all the UI panels in the array to inactive
    public void DeactivateAllPanels()
    {
        foreach(GameObject p in panels)
        {
            p.SetActive(false);
        }
    }

    //set the ismainmenu gameobject to false
    public void setMainMenuFalse()
    {
        isMainMenu = false;
    }
}
