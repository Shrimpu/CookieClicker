using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InstantCps", menuName = "Cheatcodes/InstantCps")]
public class MoreCps : Cheatcode
{
    public override void UniqueEffect()
    {
        IdleCookies.IncreaseCps(increase);
    }
}
