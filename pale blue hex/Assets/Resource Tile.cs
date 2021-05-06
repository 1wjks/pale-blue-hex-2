using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Tile", menuName = "Tile/Resource Tile")]
public class ResourceTile : Tile
{
    //PTG : per turn gain
    //IG : instant gain

    public int metalPTG;
    public int crystalPTG;
    public int moolahPTG;

    public void collectResources(GameObject pm)
    {
        PlayerManager m = pm.GetComponent<PlayerManager>();

        m.setMetal( m.getMetal() + metalPTG );
        m.setCrystal(m.getCrystal() + crystalPTG);
        m.setMoolah(m.getMoolah() + moolahPTG);
    }
}
