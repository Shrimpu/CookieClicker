using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReColour : MonoBehaviour
{
    void Start()
    {
        SpaceClicker spaco = GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceClicker>();
        spaco.LektionStart += BecomeRed;
        spaco.LektionSlut += BecomeBlue;
    }

    void BecomeRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void BecomeBlue()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
