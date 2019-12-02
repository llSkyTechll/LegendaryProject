using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillation : MonoBehaviour
{
    [SerializeField] Vector3 mouvementVector = new Vector3(0, 5f, 0);

    [Range(0, 1)]
    [SerializeField]
    Vector3 startingPos;


    void Start()
    {
        startingPos = transform.position;
    }

    public void DoorUp()
    {
        Vector3 offset = mouvementVector;
        transform.position = startingPos + offset;
    }
    public void DoorDown()
    {
        Vector3 offset = mouvementVector;
        transform.position = startingPos - offset;
    }
}
