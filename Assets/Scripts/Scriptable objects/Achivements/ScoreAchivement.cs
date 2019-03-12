using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScoreAchievement", menuName = "Achievements/ScoreAchievement")]
public class ScoreAchievement : ScriptableObject // all of these should have been derived from one object
{
    public Sprite image;
    public new string name;
    public ulong CookiesRequired;

    public string description;

    [HideInInspector]
    public bool achievementGot;
}