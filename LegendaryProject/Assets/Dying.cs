using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : MonoBehaviour {
    [SerializeField]
    private int hitpoint = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hitpoint<=0)
        {
            Destroy(gameObject);
        }
	}

    public void TakeDamage(int damage)
    {
        hitpoint -= damage;
    }
}
