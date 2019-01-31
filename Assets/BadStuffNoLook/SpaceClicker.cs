using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceClicker : MonoBehaviour
{
    public event Action LektionStart;
    public event Action LektionSlut;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (LektionStart != null)
                LektionStart.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (LektionSlut != null)
                LektionSlut.Invoke();
        }
    }
}
