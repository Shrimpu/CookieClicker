using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieHandler : MonoBehaviour
{
    public static event System.Action CookiesGained;

    public static ulong cookies;
    public static ulong totalCookies;

    void Start()
    {
        Cookie.ClickEvent += AddCookies;
    }

    public static void AddCookies(ulong cookiesToAdd)
    {
        cookies += cookiesToAdd;
        totalCookies += cookiesToAdd;

        if (CookiesGained != null)
            CookiesGained.Invoke();
    }
}
