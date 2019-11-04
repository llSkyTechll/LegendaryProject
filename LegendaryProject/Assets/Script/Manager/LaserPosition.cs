using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPosition : MonoBehaviour
{

    #region Singleton

    public static LaserPosition instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject LaserStart;
    public GameObject LaserEnd;


}
