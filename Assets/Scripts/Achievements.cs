using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    int clickTotal;
    ulong upgradesBought;

    public event System.Action CheckIfAchievementGet;

    public delegate void OnAchivementGetDelegate(string _name, string description);
    public OnAchivementGetDelegate OnAchivementGet;

    public ClicksAchievement[] clickAchievements;
    public ScoreAchievement[] scoreAchievements;
    public ScoreAchievement[] cpsAchievements;
    public UpgradesBoughtAchievement[] numOfUpgradesAchievements;

    void Start()
    {
        ResetAchievements();

        Cookie.TotalClicks += AddToClickTotal;
        IdleCookies.IdleCookieGained += CheckScoreAchievements;
        IdleCookies.CpsChanged += CheckCpsAchievements;
        UpgradeManager.JustBoughtAThing += AddToBoughtTotal;

        CheckIfAchievementGet += CheckClickAchievements;
        CheckIfAchievementGet += CheckScoreAchievements;
        CheckIfAchievementGet += CheckCpsAchievements;
        CheckIfAchievementGet += CheckUpgradeAchievements;
    }

    private void ResetAchievements()
    {
        foreach (ClicksAchievement c in clickAchievements)
        {
            c.achievementGot = false;
        }
        foreach (ScoreAchievement s in scoreAchievements)
        {
            s.achievementGot = false;
        }
        foreach (ScoreAchievement c in cpsAchievements)
        {
            c.achievementGot = false;
        }
        foreach (UpgradesBoughtAchievement u in numOfUpgradesAchievements)
        {
            u.achievementGot = false;
        }
    }

    public void AddToClickTotal()
    {
        clickTotal++;
        CheckIfAchievementGet.Invoke();
    }

    public void AddToBoughtTotal()
    {
        upgradesBought++;
        CheckIfAchievementGet.Invoke();
    }

    #region Achievements Checks

    void CheckClickAchievements()
    {
        foreach (ClicksAchievement achievement in clickAchievements)
        {
            if (achievement.clicksRequired == clickTotal)
            {
                DisplayAchievement(achievement.name, achievement.description);
                Cookie.cookiesPerClick += achievement.cpcIncrease;
                break;
            }
        }
    }

    void CheckScoreAchievements()
    {
        foreach (ScoreAchievement achievement in scoreAchievements)
        {
            if (achievement.CookiesRequired <= CookieHandler.cookies && !achievement.achievementGot)
            {
                achievement.achievementGot = true;
                DisplayAchievement(achievement.name, achievement.description);
            }
            else if (achievement.CookiesRequired > CookieHandler.cookies)
                break;
        }
    }

    void CheckCpsAchievements()
    {
        foreach (ScoreAchievement achievement in cpsAchievements)
        {
            if (achievement.CookiesRequired <= IdleCookies.cookiesPerSecond && !achievement.achievementGot)
            {
                achievement.achievementGot = true;
                DisplayAchievement(achievement.name, achievement.description);
            }
            else if (achievement.CookiesRequired > IdleCookies.cookiesPerSecond)
                break;
        }
    }

    void CheckUpgradeAchievements()
    {
        foreach (UpgradesBoughtAchievement achievement in numOfUpgradesAchievements)
        {
            if (achievement.upgradesRequired <= upgradesBought && !achievement.achievementGot)
            {
                achievement.achievementGot = true;
                IdleCookies.ChangeCpsMult(achievement.cpsMult);
                DisplayAchievement(achievement.name, achievement.description);
            }
            if (achievement.upgradesRequired > upgradesBought)
                break;
        }
    }

    #endregion

    private void DisplayAchievement(string _name, string description)
    {
        if (OnAchivementGet != null)
            OnAchivementGet.Invoke(_name, description);
    }
}
