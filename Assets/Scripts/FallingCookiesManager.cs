﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCookiesManager : MonoBehaviour
{
    public static event System.Action Under30Fps;
    public static event System.Action Under15Fps;

    public static int fpsDrop;
    public static int fpsDropSevere;

    public GameObject cookie;
    public int cookieNewNumber = 5;
    public int cookieMaxNumber = 20;
    public ulong maxCookiesPerClick = 15;
    public ulong maxCookiesPerClickOnComputerBreakdownAndTheFramesGoVeryLow = 1;
    public ulong maxCookiesPerClickOnComputerBreakdownAndTheFramesGoVeryLowEXTRADISASTER = 0;

    float cookiesSpawned;

    void Start()
    {
        IdleCookies.CpsChanged += TryToSpawnCookie;
        Cookie.ClickEvent += SpawnAntiVaxxCookie;
    }

    void TryToSpawnCookie()
    {
        if (cookiesSpawned < cookieMaxNumber && Mathf.Floor(IdleCookies.cookiesPerSecond / 5) > cookiesSpawned)
        {
            spawnCookies((int)Mathf.Floor(IdleCookies.cookiesPerSecond / 5f) - (int)cookiesSpawned);
        }
    }

    void spawnCookies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (cookiesSpawned < cookieMaxNumber)
            {
                cookiesSpawned++;
                Instantiate(cookie);
            }
        }
    }

    void SpawnAntiVaxxCookie(ulong amount)
    {
        ulong maxCookiesToSpawn = maxCookiesPerClick;
        if (Time.deltaTime > 1f / 15f)
        {
            maxCookiesToSpawn = maxCookiesPerClickOnComputerBreakdownAndTheFramesGoVeryLowEXTRADISASTER;
            fpsDropSevere++;
            if (Under15Fps != null)
                Under15Fps.Invoke();
        }
        if (Time.deltaTime > 1f / 30f)
        {
            maxCookiesToSpawn = maxCookiesPerClickOnComputerBreakdownAndTheFramesGoVeryLow;
            fpsDrop++;
            if (Under30Fps != null)
                Under30Fps.Invoke();
        }
        amount = (ulong)Mathf.Clamp(amount, 0, maxCookiesToSpawn);

        for (int i = 0; i < (int)amount; i++)
        {
            GameObject spawnedCookie = Instantiate(cookie);
            spawnedCookie.GetComponent<FallingCookie>().die = true;
        }
    }
}