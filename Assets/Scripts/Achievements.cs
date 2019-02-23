using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    ulong clickTotal;
    ulong upgradesBought;

    public delegate void OnAchivementGetDelegate(string _name, string description);
    public OnAchivementGetDelegate OnAchivementGet;

    public ClicksAchievement[] clickAchievements;
    public ScoreAchievement[] scoreAchievements;
    public ScoreAchievement[] cpsAchievements;
    public UpgradesBoughtAchievement[] numOfUpgradesAchievements;
    public ScoreAchievement[] frameDropAchievement;
    public ScoreAchievement[] frameDropAchievementSevere;

    void Start()
    {
        ResetAchievements();

        Cookie.TotalClicks += AddToClickTotal;
        Cookie.TotalClicks += CheckClickAchievements;
        IdleCookies.IdleCookieGained += CheckScoreAchievements;
        IdleCookies.CpsChanged += CheckCpsAchievements;
        UpgradeManager.JustBoughtAThing += AddToBoughtTotal;
        UpgradeManager.JustBoughtAThing += CheckUpgradeAchievements;
        FallingCookiesManager.Under30Fps += CheckFrameAchievements;
        FallingCookiesManager.Under15Fps += CheckSevereFrameAchievements;
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
        foreach (ScoreAchievement fa in frameDropAchievement)
        {
            fa.achievementGot = false;
        }
        foreach (ScoreAchievement fa in frameDropAchievementSevere)
        {
            fa.achievementGot = false;
        }
    }

    public void AddToClickTotal()
    {
        clickTotal++;
    }

    public void AddToBoughtTotal()
    {
        upgradesBought++;
    }

    #region Achievements Checks

    int clickAchievementsGot = 0;
    void CheckClickAchievements()
    {
        for (int i = clickAchievementsGot; i < clickAchievements.Length; i++)
        {
            if (clickAchievements[i].clicksRequired == clickTotal)
            {
                DisplayAchievement(clickAchievements[i].name, clickAchievements[i].description);
                Cookie.cookiesPerClick += clickAchievements[i].cpcIncrease;
                clickAchievementsGot = i;
                break;
            }
        }
    }

    int scoreAchievementsGot = 0;
    void CheckScoreAchievements()
    {
        for (int i = scoreAchievementsGot; i < scoreAchievements.Length; i++)
        {
            if (scoreAchievements[i].CookiesRequired <= CookieHandler.totalCookies && !scoreAchievements[i].achievementGot)
            {
                scoreAchievements[i].achievementGot = true;
                DisplayAchievement(scoreAchievements[i].name, scoreAchievements[i].description);
                scoreAchievementsGot = i;
                break;
            }
        }
    }

    int cpsAchievementsGot = 0;
    void CheckCpsAchievements()
    {
        for (int i = cpsAchievementsGot; i < cpsAchievements.Length; i++)
        {
            if (cpsAchievements[i].CookiesRequired <= IdleCookies.cookiesPerSecond && !cpsAchievements[i].achievementGot)
            {
                cpsAchievements[i].achievementGot = true;
                DisplayAchievement(cpsAchievements[i].name, cpsAchievements[i].description);
                cpsAchievementsGot = i;
                break;
            }
        }
    }

    int upgradeAchievementsGot = 0;
    void CheckUpgradeAchievements()
    {
        for (int i = upgradeAchievementsGot; i < numOfUpgradesAchievements.Length; i++)
        {
            if (numOfUpgradesAchievements[i].upgradesRequired <= upgradesBought && !numOfUpgradesAchievements[i].achievementGot)
            {
                numOfUpgradesAchievements[i].achievementGot = true;
                IdleCookies.ChangeCpsMult(numOfUpgradesAchievements[i].cpsMult);
                DisplayAchievement(numOfUpgradesAchievements[i].name, numOfUpgradesAchievements[i].description);
                upgradeAchievementsGot = i;
                break;
            }
        }
    }

    void CheckFrameAchievements()
    {
        foreach (ScoreAchievement fa in frameDropAchievement)
        {
            if (fa.CookiesRequired == (ulong)FallingCookiesManager.fpsDrop)
            {
                DisplayAchievement(fa.name, fa.description);
                break;
            }
        }
    }

    void CheckSevereFrameAchievements()
    {
        foreach (ScoreAchievement fa in frameDropAchievementSevere)
        {
            if (fa.CookiesRequired == (ulong)FallingCookiesManager.fpsDropSevere)
            {
                DisplayAchievement(fa.name, fa.description);
                break;
            }
        }
    }

    #endregion

    private void DisplayAchievement(string _name, string description)
    {
        if (OnAchivementGet != null)
            OnAchivementGet.Invoke(_name, description);
    }
}
