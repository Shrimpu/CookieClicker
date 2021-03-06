﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    public static ulong clickTotal;
    public static ulong upgradesBought;
    public bool resetAchievementsOnLoad;

    public delegate void OnAchivementGetDelegate(string _name, string description, Sprite image);
    public OnAchivementGetDelegate OnAchivementGet;

    // all types of achievements
    public ClicksAchievement[] clickAchievements;
    public ScoreAchievement[] scoreAchievements;
    public ScoreAchievement[] cpsAchievements;
    public UpgradesBoughtAchievement[] numOfUpgradesAchievements;
    public ScoreAchievement[] frameDropAchievement;
    public ScoreAchievement[] frameDropAchievementSevere;

    void Start()
    {
        if (resetAchievementsOnLoad)
            ResetAchievements();

        // sets up all events
        Cookie.TotalClicks += AddToClickTotal;
        Cookie.TotalClicks += CheckClickAchievements;
        Cookie.TotalClicks += CheckScoreAchievements;
        CookieHandler.CookiesGained += CheckScoreAchievements;
        IdleCookies.CpsChanged += CheckCpsAchievements;
        UpgradeManager.JustBoughtAThing += AddToBoughtTotal;
        UpgradeManager.JustBoughtAThing += CheckUpgradeAchievements;
        FallingCookiesManager.Under30Fps += CheckFrameAchievements;
        FallingCookiesManager.Under15Fps += CheckSevereFrameAchievements;
    }

    public void ResetAchievements()
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

        clickAchievementsGot = 0;
        scoreAchievementsGot = 0;
        cpsAchievementsGot = 0;
        upgradeAchievementsGot = 0;
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

    int clickAchievementsGot = 0; // this helps us start from latest achievement got in the for-loop
    void CheckClickAchievements()
    {
        for (int i = clickAchievementsGot; i < clickAchievements.Length; i++)
        {
            if (clickAchievements[i].clicksRequired == clickTotal)
            {
                Cookie.cookiesPerClick += clickAchievements[i].cpcIncrease;
                clickAchievementsGot = i;
                DisplayAchievement(clickAchievements[i].name, clickAchievements[i].description, clickAchievements[i].image);
                break; // break to stop unecessary checks on achievements we know we havn't fulfilled.
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
                scoreAchievementsGot = i;
                DisplayAchievement(scoreAchievements[i].name, scoreAchievements[i].description, scoreAchievements[i].image);
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
                cpsAchievementsGot = i;
                DisplayAchievement(cpsAchievements[i].name, cpsAchievements[i].description, cpsAchievements[i].image);
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
                upgradeAchievementsGot = i;
                DisplayAchievement(numOfUpgradesAchievements[i].name, numOfUpgradesAchievements[i].description, numOfUpgradesAchievements[i].image);
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
                fa.achievementGot = true;
                DisplayAchievement(fa.name, fa.description, fa.image);
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
                fa.achievementGot = true;
                DisplayAchievement(fa.name, fa.description, fa.image);
                break;
            }
        }
    }

    #endregion

    private void DisplayAchievement(string _name, string description, Sprite image)
    {
        if (OnAchivementGet != null)
            OnAchivementGet.Invoke(_name, description, image);
    }
}
