using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CpsUpgrade", menuName = "Upgrades/CpsUpgrade")]
public class CpsUp : UpgradeEffect
{
    public override void Do(ulong i)
    {
        IdleCookies.IncreaseCps(i);
    }
}
