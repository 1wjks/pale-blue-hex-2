using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tile", menuName = "Tile")]
public class Tile : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite tileArt;


}
