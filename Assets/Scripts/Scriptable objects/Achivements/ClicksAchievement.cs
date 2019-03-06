using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClicksAchievement", menuName = "Achievements/ClicksAchievement")]
public class ClicksAchievement : ScriptableObject
{
    public Sprite image;
    public new string name;
    public ulong clicksRequired;
    public ulong cpcIncrease = 0;

    public string description;

    [HideInInspector]
    public bool achievementGot;
}