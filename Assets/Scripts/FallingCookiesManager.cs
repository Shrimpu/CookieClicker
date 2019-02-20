using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCookiesManager : MonoBehaviour
{
    public GameObject cookie;
    public int cookieNewNumber = 5;
    public int cookieMaxNumber = 20;

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
        for (int i = 0; i < (int)amount; i++)
        {
            GameObject spawnedCookie = Instantiate(cookie);
            spawnedCookie.GetComponent<FallingCookie>().die = true;
        }
    }
}
