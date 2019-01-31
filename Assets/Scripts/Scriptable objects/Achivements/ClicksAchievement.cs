using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClicksAchievement", menuName = "Achievements/ClicksAchievement")]
public class ClicksAchievement : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int clicksRequired;

    [Multiline]
    public string description;
}