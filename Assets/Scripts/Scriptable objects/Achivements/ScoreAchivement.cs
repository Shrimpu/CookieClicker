using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScoreAchievement", menuName = "Achievements/ScoreAchievement")]
public class ScoreAchievement : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public int CookiesRequired;

    [Multiline]
    public string description;
}