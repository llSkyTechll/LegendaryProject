using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


    public  List<GameObject> Ennemis = new List<GameObject>();

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
