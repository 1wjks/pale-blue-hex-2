using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] TextArray; 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTextField(int arrayNumber, string s)
    {
        TextArray[arrayNumber].text = s;
    }
}
