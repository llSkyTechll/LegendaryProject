using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyAI
{

    // Use this for initialization


    // Update is called once per frame
    protected override string GetAnimationRunName()
    {
        return "Run";
    }
}
