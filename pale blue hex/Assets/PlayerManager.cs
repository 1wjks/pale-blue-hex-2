using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GManager gm;
    [SerializeField]
    private UIcontroller uic;

    [SerializeField]
    private int metal = 0;
    [SerializeField]
    private int crystal = 0;
    [SerializeField]
    private int moolah = 0;

    
    public void Start()
    {
        
        //Finds GM & UIC if not set at spawn
        if (gm == null)
        {
            Debug.Log("GM is NULL");

            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach(GameObject obj in allObjects)
            {
                if (obj.activeInHierarchy)
                {
                    if(obj.GetComponent<GManager>() != null)
                    {
                        Debug.Log("GM manager Found");
                        gm = obj.GetComponent<GManager>();
                        break;
                    }
                }
            }
        }

        if (uic == null)
        {
            Debug.Log("UIC is NULL");
            GameObject[] allObjects = FindObjectsOfType<GameObject>();
            foreach(GameObject obj in allObjects)
            {
                if (obj.activeInHierarchy)
                {
                    if(obj.GetComponent<UIcontroller>() != null)
                    {
                        uic = obj.GetComponent<UIcontroller>();
                        break;
                    }
                }
            }
        }

        UpdateUIText();
    }

    public void Update()
    {
        // if space bar pressedd
        if (Input.GetKeyDown(KeyCode.Space))
        {
            setCrystal(getCrystal() + 1);
            UpdateUIText();
        }
    }

    public void startTurn()
    {
        
    }

    [Command]
    public void CMDEndTurn()
    {
        gm.GetComponent<GManager>().EndTurn();
    }

    private void UpdateUIText()
    {
        uic.changeTextField(0, "" + moolah);
        uic.changeTextField(1, "" + metal);
        uic.changeTextField(2, "" + crystal);
    }

    public int getMetal()
    {
        return metal;
    }
    public void setMetal(int set)
    {
        metal = set;
    }
    public int getCrystal()
    {
        return crystal;
    }
    public void setCrystal(int set)
    {
        crystal = set;
    }
    public int getMoolah()
    {
        return moolah;
    }
    public void setMoolah(int set)
    {
        moolah = set;
    }
}
