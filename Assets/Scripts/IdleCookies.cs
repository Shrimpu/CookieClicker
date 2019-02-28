using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCookies : MonoBehaviour
{
    public static event System.Action IdleCookieGained;
    public static event System.Action CpsChanged;

    public static ulong cookiesPerSecond = 0;
    public static ulong cookieBoost = 1;
    [Range(1f, 50f)]
    public float executionTimesPerSecond = 10f;

    void Start()
    {
        StartCoroutine(GiveIdleCookies());
    }

    IEnumerator GiveIdleCookies()
    {
        float cookiesOnHold = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f / executionTimesPerSecond);

            cookiesOnHold += (cookiesPerSecond / executionTimesPerSecond);

            if (cookiesOnHold >= 1f)
            {
                CookieHandler.AddCookies((ulong)Mathf.Floor(cookiesOnHold));
                cookiesOnHold = cookiesOnHold % 1f; // saves the decimals that wern't added due to the cast to ulong

                if (IdleCookieGained != null)
                    IdleCookieGained.Invoke();
            }
        }
    }

    public static void ChangeCpsMult(ulong mult)
    {
        cookiesPerSecond *= mult;
        cookieBoost *= mult;
        if (CpsChanged != null)
            CpsChanged.Invoke();
    }

    public static void IncreaseCps(ulong amount)
    {
        cookiesPerSecond += amount * cookieBoost;
        if (CpsChanged != null)
            CpsChanged.Invoke();
    }
}
