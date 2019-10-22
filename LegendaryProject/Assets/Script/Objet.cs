using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour {

    protected enum Rarity { Common, Uncommon, Rare, Mythic, Legendary};
    protected Rarity rarity;
    protected System.Random randomRarity = new System.Random();
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
