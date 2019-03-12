using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CpsClickUpgrade", menuName = "Upgrades/CpsClickUpgrade")]
public class CpsClickUp : UpgradeEffect
{
    public override void Do(ulong i)
    {
        Cookie.cookiesPerClick += i;
        IdleCookies.IncreaseCps(i);
    }
}
