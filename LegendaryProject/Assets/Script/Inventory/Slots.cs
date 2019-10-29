using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slots : MonoBehaviour {
    public GameObject item;
    public bool empty;
    public Sprite icon;
    public int ID;
    public string type;
    public string description;

    public Transform slotIconGo;
    // Use this for initialization
    void Start () {
        slotIconGo = transform.GetChild(0);
	}
	
	// Update is called once per frame
    public void UpdateSlot()
    {
        slotIconGo.GetComponent<Image>().sprite = icon;
    }
    public void UseItem()
    {

    }
}
