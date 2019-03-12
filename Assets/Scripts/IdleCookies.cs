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

    IEnumerator GiveIdleCookies() // this function is originally written by joar (me).
    {
        float cookiesOnHold = 0;
        while (true)
        {
            yield return new WaitForSeconds(1f / executionTimesPerSecond); // if exectimes = 10, wait 0.1s

            cookiesOnHold += (cookiesPerSecond / executionTimesPerSecond); // saves the exact number of cookies earned

            if (cookiesOnHold >= 1f)
            {
                CookieHandler.AddCookies((ulong)Mathf.Floor(cookiesOnHold)); // if cookiesOnHold = 1.33. it adds 1.
                cookiesOnHold = cookiesOnHold % 1f; // saves the decimals that weren't added

                if (IdleCookieGained != null)
                    IdleCookieGained.Invoke();
            }
        }
    }

    public static void ChangeCpsMult(ulong mult)
    {
        cookiesPerSecond *= mult;
        cookieBoost *= mult; // cookieboost allows me to permanently add more cookies
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
