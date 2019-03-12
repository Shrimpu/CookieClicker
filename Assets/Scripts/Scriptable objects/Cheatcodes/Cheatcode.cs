using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cheatcode : ScriptableObject
{
    public string code;
    public ulong increase;

    [HideInInspector]
    public KeyCode[] keycodes;
    [HideInInspector]
    public int progress = 0;

    public void SetUp() // assign keycode array
    {
        progress = 0;
        keycodes = new KeyCode[code.Length];
        for (int i = 0; i < code.Length; i++)
        {
            keycodes[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), code[i].ToString().ToUpper());
        }
    }

    public abstract void UniqueEffect(); // what the cheatcode does
}
