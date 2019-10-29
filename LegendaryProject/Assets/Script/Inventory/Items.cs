using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;

    [HideInInspector]
    public bool equipped;
    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public GameObject weaponManager;

    public bool playersWeapon;
   
	// Use this for initialization
	void Start () {
        if (!playersWeapon)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
        if (equipped)
        {
// preform weapon acts here
        }
	}
    public void ItemUsage()
    {
        // weapon

        if (type == "Weapon")
        {
            equipped = true;
        }
    }

}
