using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClickUpgrade", menuName = "Upgrades/ClickUpgrade")]
public class ClickUp : UpgradeEffect
{
    public override void Do(ulong i)
    {
        Cookie.cookiesPerClick += i;
    }
}
