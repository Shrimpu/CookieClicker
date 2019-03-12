using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hax : MonoBehaviour
{
    public Cheatcode[] cheatcodes;

    private void Awake()
    {
        for (int i = 0; i < cheatcodes.Length; i++)
        {
            cheatcodes[i].SetUp();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < cheatcodes.Length; i++)
            {
                KeyCode kc = cheatcodes[i].keycodes[cheatcodes[i].progress];
                if (Input.GetKeyDown(kc))
                {
                    cheatcodes[i].progress++;
                    if (cheatcodes[i].progress == cheatcodes[i].keycodes.Length)
                    {
                        cheatcodes[i].progress = 0;
                        cheatcodes[i].UniqueEffect();
                    }
                }
                else
                {
                    cheatcodes[i].progress = 0;
                }
            }
        }
    }
}