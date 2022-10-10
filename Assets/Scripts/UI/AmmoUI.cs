using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    Transform[] ammoUI = new Transform[2];
    Transform[] superAmmoUI = new Transform[2];

    // Start is called before the first frame update
    void Start()
    {
        ammoUI[0] = GameObject.Find("Ammo 1").transform;
        ammoUI[1] = GameObject.Find("Ammo 2").transform;
        superAmmoUI[0] = GameObject.Find("Super Ammo 1").transform;
        superAmmoUI[1] = GameObject.Find("Super Ammo 2").transform;
    }

    public void UpdateAmmoUI()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Player.instance.ammo[i] == 'R')
            {
                ammoUI[i].localScale = new Vector3(1, 1, 0);
                superAmmoUI[i].localScale = new Vector3(0, 0, 0);
            }
            else if (Player.instance.ammo[i] == 'S')
            {
                ammoUI[i].localScale = new Vector3(0, 0, 0);
                superAmmoUI[i].localScale = new Vector3(1, 1, 0);
            }
            else if (Player.instance.ammo[i] == 'N')
            {
                ammoUI[i].localScale = new Vector3(0, 0, 0);
                superAmmoUI[i].localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
