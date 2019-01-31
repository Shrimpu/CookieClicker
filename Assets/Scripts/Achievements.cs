using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    float clickTotal;

    public delegate void OnAchivementGetDelegate(string _name, string description);
    public OnAchivementGetDelegate OnAchivementGet;

    #region Achivements

    public ClicksAchievement[] clickAchievements;
    public ScoreAchievement[] scoreAchievements;

    #endregion

    void Start()
    {
        Cookie cookie = FindObjectOfType<Cookie>();
        cookie.TotalClicks += AddToClickTotal;
    }

    public void AddToClickTotal()
    {
        clickTotal++;
    }

    private void CheckAchievementStatus(int clicks)
    {
        foreach (ClicksAchievement achievement in clickAchievements)
        {
            if (achievement.clicksRequired == clicks)
            {
                DisplayAchievement(achievement.name, achievement.description);
                break;
            }
        }

        foreach (ScoreAchievement achievement in scoreAchievements)
        {
            if (achievement.CookiesRequired == CookieHandler.cookies)
            {
                DisplayAchievement(achievement.name, achievement.description);
                break;
            }
        }
    }

    private void DisplayAchievement(string _name, string description)
    {
        if (OnAchivementGet != null)
            OnAchivementGet.Invoke(_name, description);

        print(_name);
    }
}
