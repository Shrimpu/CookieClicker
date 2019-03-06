using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UpgradesBoughtAchievement", menuName = "Achievements/UpgradesBoughtAchievement")]
public class UpgradesBoughtAchievement : ScriptableObject
{
    public Sprite image;
    public new string name;
    public ulong upgradesRequired;
    public ulong cpsMult = 1;

    public string description;

    [HideInInspector]
    public bool achievementGot;
}