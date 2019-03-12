using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InstantCookies", menuName = "Cheatcodes/InstantCookies")]
public class InstantCookies : Cheatcode
{
    public override void UniqueEffect()
    {
        CookieHandler.AddCookies(increase);
    }
}
