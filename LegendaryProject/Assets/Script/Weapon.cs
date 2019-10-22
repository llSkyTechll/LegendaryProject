using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment { 

    public int minDamage;
    public int maxDamage;
    protected float range;

	// Use this for initialization
	void Start () {
        rarity = (Rarity)randomRarity.Next(0,5);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
