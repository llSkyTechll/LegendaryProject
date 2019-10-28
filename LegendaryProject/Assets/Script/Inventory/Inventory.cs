using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public bool inventoryEnabled;
    public GameObject inventory;

    private int allSlots;
    private GameObject[] slot;

    public GameObject slotHolder;
    void Start()
    {
        allSlots = 30;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slots>().item == null)
            {
                slot[i].GetComponent<Slots>().empty = true;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Items item = itemPickedUp.GetComponent<Items>();

            AddItem(itemPickedUp,item.ID, item.type, item.description, item.icon);
        }
    }
    void AddItem(GameObject ItemObject,int ItemId, string ItemType, string ItemDescription, Texture2D ItemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slots>().empty)
            {
                //add item to slot
                ItemObject.GetComponent<Items>().pickedUp = true;

                slot[i].GetComponent<Slots>().item = ItemObject;
                slot[i].GetComponent<Slots>().icon = ItemIcon;
                slot[i].GetComponent<Slots>().type = ItemType;
                slot[i].GetComponent<Slots>().description = ItemDescription;
                slot[i].GetComponent<Slots>().ID = ItemId;

                ItemObject.transform.parent = slot[i].transform;
                ItemObject.SetActive(false);

                slot[i].GetComponent<Slots>().empty = false;
            }
        }
    }
}
