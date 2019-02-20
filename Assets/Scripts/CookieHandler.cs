using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieHandler : MonoBehaviour
{
    public static ulong cookies;

    void Start()
    {
        Cookie.ClickEvent += AddCookies;
    }

    void AddCookies(ulong cookiesToAdd)
    {
        cookies += cookiesToAdd;
    }
}
